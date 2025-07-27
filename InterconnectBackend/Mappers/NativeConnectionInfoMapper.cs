using Models;
using NativeLibrary.Structs;

namespace Mappers
{
    public static class NativeConnectionInfoMapper
    {
        public static ConnectionInfo MapToConnectionInfo(NativeConnectionInfo info)
        {
            return new ConnectionInfo
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
}
