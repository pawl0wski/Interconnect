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
    /// Background service for managing virtual machine console data streaming.
    /// </summary>
    public class VirtualMachineConsoleBackgroundService : BackgroundService
    {
        private readonly ILogger<VirtualMachineConsoleBackgroundService> _logger;
        private readonly IVirtualMachineConsoleService _vmConsoleService;
        private readonly IHubContext<VirtualMachineConsoleHub> _hubContext;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        private Dictionary<Guid, Task> _dataRecievers = new();

        /// <summary>
        /// Initializes a new instance of the VirtualMachineConsoleBackgroundService.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="vmConsoleService">Virtual machine console service.</param>
        /// <param name="hubContext">SignalR hub context for console communication.</param>
        public VirtualMachineConsoleBackgroundService(
            ILogger<VirtualMachineConsoleBackgroundService> logger,
            IVirtualMachineConsoleService vmConsoleService,
            IHubContext<VirtualMachineConsoleHub> hubContext)
        {
            _logger = logger;
            _vmConsoleService = vmConsoleService;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Executes the background service to listen for and stream console data.
        /// </summary>
        /// <param name="stoppingToken">Cancellation token for stopping the service.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                _logger.LogInformation("Listening for console data");
                while (!stoppingToken.IsCancellationRequested)
                {
                    var streams = _vmConsoleService.GetStreams();
                    foreach (var stream in streams)
                    {
                        AddNewStreamToReceiversIfNotExist(stream, stoppingToken);
                    }
                    await Task.Delay(500);
                }
            }, stoppingToken);

        }

        /// <summary>
        /// Adds a new stream to receivers if it doesn't already exist.
        /// </summary>
        /// <param name="stream">Stream information to add.</param>
        /// <param name="stoppingToken">Cancellation token.</param>
        private void AddNewStreamToReceiversIfNotExist(StreamInfo stream, CancellationToken stoppingToken)
        {
            if (!_dataRecievers.ContainsKey(stream.Uuid))
            {
                _dataRecievers[stream.Uuid] = LaunchConsoleDataReceiver(stream, stoppingToken);
                _logger.LogInformation("Added new console stream to receivers {StreamUuid}", stream.Uuid);
            }
        }

        /// <summary>
        /// Launches a console data receiver task for a specific stream.
        /// </summary>
        /// <param name="stream">Stream information.</param>
        /// <param name="stoppingToken">Cancellation token.</param>
        /// <returns>Task representing the receiver operation.</returns>
        private async Task LaunchConsoleDataReceiver(StreamInfo stream, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!await SendChunkToClients(stream))
                {
                    _vmConsoleService.CloseStream(stream);
                    _dataRecievers.Remove(stream.Uuid);
                    _logger.LogWarning("Closed console receiver for stream {StreamUuid}", stream.Uuid);
                    break;
                }
            }
        }

        /// <summary>
        /// Sends a chunk of console data to connected clients.
        /// </summary>
        /// <param name="stream">Stream information.</param>
        /// <returns>True if data was sent successfully, false if stream is broken.</returns>
        private async Task<bool> SendChunkToClients(StreamInfo stream)
        {
            var uuid = stream.Uuid.ToString();
            var data = await Task.Run(() => _vmConsoleService.GetBytesFromConsole(stream));

            if (data.IsStreamBroken)
            {
                return false;
            }

            var response = CreateSerializedTerminalDataResponse(uuid, data.Data);
            await _hubContext.Clients.Group(uuid).SendAsync("NewTerminalData", response);
            return true;
        }

        /// <summary>
        /// Creates a serialized terminal data response.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <param name="data">Console data bytes.</param>
        /// <returns>Serialized response as JSON string.</returns>
        private string CreateSerializedTerminalDataResponse(string uuid, byte[] data)
        {
            var response = TerminalDataResponse.WithSuccess(
                new TerminalData
                {
                    Uuid = uuid,
                    Data = Convert.ToBase64String(data),
                });

            return JsonSerializer.Serialize(response, _jsonSerializerOptions);
        }
    }
}
