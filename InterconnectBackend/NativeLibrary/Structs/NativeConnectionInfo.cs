using System.Runtime.InteropServices;

namespace NativeLibrary.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NativeConnectionInfo
    {
        public uint CpuCount;
        public uint CpuFreq;
        public long TotalMemory;
        [MarshalAs(UnmanagedType.LPStr)]
        public string ConnectionUrl;
        [MarshalAs(UnmanagedType.LPStr)]
        public string DriverType;
        public NativeVersion LibVersion;
        public NativeVersion DriverVersion;
    }
}
