using NativeLibrary.Structs;

namespace Models.Mappers
{
    public static class NativeConnectionInfoToConnectionInfoMapper
    {
        public static ConnectionInfo Map(NativeConnectionInfo info)
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
