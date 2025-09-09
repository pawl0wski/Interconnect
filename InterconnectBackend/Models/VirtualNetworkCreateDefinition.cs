namespace Models
{
    public class VirtualNetworkCreateDefinition
    {
        public required string NetworkName { get; set; }
        public required string BridgeName { get; set; }
        public bool? ForwardNat { get; set; }
        public string? MacAddress { get; set; }
        public string? IpAddress { get; set; }
        public string? NetMask { get; set; }
        public string? DhcpStartRange { get; set; }
        public string? DhcpEndRange { get; set; }
    }
}
