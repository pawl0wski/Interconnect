namespace Models.Responses
{
    public class ConnectionInfoResponse
    {
        public uint CpuCount { get; set; }
        public uint CpuFreq { get; set; }
        public long TotalMemory { get; set; }
        public string ConnectionUrl { get; set; }
        public string DriverType { get; set; }
        public string LibVersion { get; set; }
        public string DriverVersion { get; set; }
    }
}
