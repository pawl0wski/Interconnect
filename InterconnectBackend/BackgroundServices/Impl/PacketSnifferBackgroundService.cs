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

        private void ParseCapturedPackets()
        {
            while (true)
            {
                var packet = _packetSnifferService.GetCapturedPacket();
                if (packet is null) break;
                SendCapturedPacketToDefaultHubGroup(packet);
            }
        }

        private void SendCapturedPacketToDefaultHubGroup(Packet packet)
        {
            var response = CapturedPacketResponse.WithSuccess(packet);

            _hubContext.Clients.Group("default").SendAsync("NewPacket", JsonSerializer.Serialize(response, _jsonSerializerOptions));
        }
    }
}
