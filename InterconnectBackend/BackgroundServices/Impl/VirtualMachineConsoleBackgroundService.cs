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

        public VirtualMachineConsoleBackgroundService(
            ILogger<VirtualMachineConsoleBackgroundService> logger,
            IVirtualMachineConsoleService vmConsoleService,
            IHubContext<VirtualMachineConsoleHub> hubContext)
        {
            _logger = logger;
            _vmConsoleService = vmConsoleService;
            _hubContext = hubContext;
        }

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

        private void AddNewStreamToReceiversIfNotExist(StreamInfo stream, CancellationToken stoppingToken)
        {
            if (!_dataRecievers.ContainsKey(stream.Uuid))
            {
                _dataRecievers[stream.Uuid] = LaunchConsoleDataReceiver(stream, stoppingToken);
                _logger.LogInformation("Added new console stream to receivers {StreamUuid}", stream.Uuid);
            }
        }

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
