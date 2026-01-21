using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Models.Responses;
using Services;
using System.Text.Json;

namespace BackgroundServices.Impl
{
    /// <summary>
    /// Background service for managing network packet sniffing and broadcasting.
    /// </summary>
    public class PacketSnifferBackgroundService : BackgroundService
    {
        private List<PacketSniffer> _packetSniffers = [];
        private readonly ILogger<PacketSnifferBackgroundService> _logger;
        private readonly IHubContext<PacketSnifferHub> _hubContext;
        private readonly IPacketSnifferService _packetSnifferService;
        private readonly IVirtualNetworkService _virtualNetworkService;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// Initializes a new instance of the PacketSnifferBackgroundService.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="hubContext">SignalR hub context for packet transmission.</param>
        /// <param name="packetSnifferService">Packet sniffer service.</param>
        /// <param name="virtualNetworkService">Virtual network service.</param>
        public PacketSnifferBackgroundService(
            ILogger<PacketSnifferBackgroundService> logger,
            IHubContext<PacketSnifferHub> hubContext,
            IPacketSnifferService packetSnifferService,
            IVirtualNetworkService virtualNetworkService)
        {
            _logger = logger;
            _hubContext = hubContext;
            _packetSnifferService = packetSnifferService;
            _virtualNetworkService = virtualNetworkService;
        }

        /// <summary>
        /// Executes the background service to listen for and broadcast captured packets.
        /// </summary>
        /// <param name="stoppingToken">Cancellation token for stopping the service.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Listening for new packets");

            await StartSniffersForAllNetworks();

            while (true)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return;
                }

                var handlers = _packetSnifferService.GetOpenedSniffers();
                foreach (var handler in handlers)
                {
                    StartListeningForSnifferIfCurrentlyNotListening(handler);
                }

                ParseCapturedPackets();

                await Task.Delay(1000, stoppingToken);
            }
        }

        /// <summary>
        /// Starts packet sniffers for all virtual networks.
        /// </summary>
        /// <returns>Task representing the operation.</returns>
        private async Task StartSniffersForAllNetworks()
        {
            var networks = await _virtualNetworkService.GetAllVirtualNetworks();

            foreach (var network in networks)
            {
                try
                {
                    _packetSnifferService.StartListeningForBridge(network.BridgeName);
                }
                catch (Exception e)
                {
                    _logger.LogError("Can't start packet sniffer for bridge {BridgeName}. {ExceptionMessage}", network.BridgeName, e.Message);
                }
            }
        }

        /// <summary>
        /// Starts listening for packets on a sniffer if not already listening.
        /// </summary>
        /// <param name="sniffer">Packet sniffer instance.</param>
        private void StartListeningForSnifferIfCurrentlyNotListening(PacketSniffer sniffer)
        {
            if (_packetSniffers.FirstOrDefault(s => s.BridgeName == sniffer.BridgeName) is not null)
            {
                return;
            }

            Task.Run(() => ListenForPackets(sniffer));
            _packetSniffers.Add(sniffer);

            _logger.LogInformation("Started listening for interface {BridgeName}", sniffer.BridgeName);
        }

        /// <summary>
        /// Continuously listens for packets on a specific sniffer.
        /// </summary>
        /// <param name="sniffer">Packet sniffer instance.</param>
        private void ListenForPackets(PacketSniffer sniffer)
        {
            while (true)
            {
                if (!_packetSnifferService.ListenForPacket(sniffer))
                {
                    _logger.LogWarning("Closed connection to packet sniffer {BridgeName}", sniffer.BridgeName);
                    return;
                }
            }
        }

        /// <summary>
        /// Parses and processes all captured packets from the sniffer.
        /// </summary>
        private void ParseCapturedPackets()
        {
            while (true)
            {
                var packet = _packetSnifferService.GetCapturedPacket();
                if (packet is null) break;
                SendCapturedPacketToDefaultHubGroup(packet);
            }
        }

        /// <summary>
        /// Sends a captured packet to all connected clients in the default hub group.
        /// </summary>
        /// <param name="packet">Captured packet data.</param>
        private void SendCapturedPacketToDefaultHubGroup(Packet packet)
        {
            var response = CapturedPacketResponse.WithSuccess(packet);

            _hubContext.Clients.Group("default").SendAsync("NewPacket", JsonSerializer.Serialize(response, _jsonSerializerOptions));
        }
    }
}
