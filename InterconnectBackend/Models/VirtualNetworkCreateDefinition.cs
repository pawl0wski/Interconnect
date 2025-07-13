namespace Models
{
    public class VirtualNetworkCreateDefinition
    {
        public required string NetworkName { get; set; }
        public required string MacAddress { get; set; }
        public required string IpAddress { get; set; }
        public required bool DhcpEnabled { get; set; }
        public string? DhcpStartRange { get; set; }
        public string? DhcpEndRange { get; set; }
    }
}
