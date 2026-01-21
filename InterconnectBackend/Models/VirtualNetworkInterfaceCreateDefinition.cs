namespace Models
{
    /// <summary>
    /// Definition for creating a network interface for a virtual machine.
    /// </summary>
    public class VirtualNetworkInterfaceCreateDefinition
    {
        /// <summary>
        /// Network interface MAC address.
        /// </summary>
        public required string MacAddress { get; set; }
        
        /// <summary>
        /// Virtual network name to connect to.
        /// </summary>
        public required string NetworkName { get; set; }
    }
}
