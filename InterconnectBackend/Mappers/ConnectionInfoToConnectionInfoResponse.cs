using Library.Models;
using Models.Responses;

namespace Mappers
{
    public static class ConnectionInfoToConnectionInfoResponse
    {
        public static ConnectionInfoResponse Map(ConnectionInfo info)
            => new()
            {
                CpuCount = info.CpuCount,
                CpuFreq = info.CpuFreq,
                TotalMemory = info.TotalMemory,
                ConnectionUrl = info.ConnectionUrl,
                DriverType = info.DriverType,
                LibVersion = info.LibVersion.ToString(),
                DriverVersion = info.DriverVersion.ToString()
            };
    }
}
