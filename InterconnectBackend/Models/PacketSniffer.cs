namespace Models
{
    /// <summary>
    /// Represents a packet sniffer for a specific network bridge.
    /// </summary>
    public class PacketSniffer
    {
        /// <summary>
        /// Name of the network bridge being monitored.
        /// </summary>
        public required string BridgeName { get; set; }
        
        /// <summary>
        /// Pointer to the native packet sniffer handler.
        /// </summary>
        public required IntPtr Handler { get; set; }
    }
}
