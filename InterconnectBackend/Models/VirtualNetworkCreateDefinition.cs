namespace Models
{
    /// <summary>
    /// Definition for creating a new virtual network.
    /// </summary>
    public class VirtualNetworkCreateDefinition
    {
        /// <summary>
        /// Virtual network name.
        /// </summary>
        public required string NetworkName { get; set; }
        
        /// <summary>
        /// Network bridge name.
        /// </summary>
        public required string BridgeName { get; set; }
        
        /// <summary>
        /// Whether to enable NAT for the network.
        /// </summary>
        public bool? ForwardNat { get; set; }
        
        /// <summary>
        /// Network interface MAC address.
        /// </summary>
        public string? MacAddress { get; set; }
        
        /// <summary>
        /// Network IP address.
        /// </summary>
        public string? IpAddress { get; set; }
        
        /// <summary>
        /// Subnet mask.
        /// </summary>
        public string? NetMask { get; set; }
        
        /// <summary>
        /// DHCP range start address.
        /// </summary>
        public string? DhcpStartRange { get; set; }
        
        /// <summary>
        /// DHCP range end address.
        /// </summary>
        public string? DhcpEndRange { get; set; }
    }
}
