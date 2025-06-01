using LibVersion = Models.Version;
using DriverVersion = Models.Version;
using System.Runtime.InteropServices;

namespace Library.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConnectionInfo
    {
        public uint CpuCount;
        public uint CpuFreq;
        public long TotalMemory;
        [MarshalAs(UnmanagedType.LPStr)]
        public string ConnectionUrl;
        [MarshalAs(UnmanagedType.LPStr)]
        public string DriverType;
        public LibVersion LibVersion;
        public DriverVersion DriverVersion;
    }
}
