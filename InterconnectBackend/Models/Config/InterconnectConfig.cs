namespace Models.Config
{
    public class InterconnectConfig
    {
        public required string VmPrefix { get; set; }
        public required string HypervisorUrl { get; set; }
        public required string DatabaseConnectionUrl { get; set; }
        public required int MaxConsoleDataHistory { get; set; }
        public required string InternetEntityDefaultIp { get; set; }
        public required string InternetEntityDefaultNetmask { get; set; }
    }
}
