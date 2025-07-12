namespace Models
{
    public class ConnectionInfo
    {
        public uint CpuCount { get; set; }
        public uint CpuFreq { get; set; }
        public long TotalMemory { get; set; }
        public required string ConnectionUrl { get; set; }
        public required string DriverType { get; set; }
        public required string LibVersion { get; set; }
        public required string DriverVersion { get; set; }
    }
}
