using Models.Enums;

namespace Models
{
    /// <summary>
    /// Represents a captured network packet.
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// Packet identifier.
        /// </summary>
        public required int Id { get; set; }
        
        /// <summary>
        /// Data link layer packet type.
        /// </summary>
        public required DataLinkLayerPacketType DataLinkLayerPacketType { get; set; }
        
        /// <summary>
        /// Source MAC address.
        /// </summary>
        public required string SourceMacAddress { get; set; }
        
        /// <summary>
        /// Destination MAC address.
        /// </summary>
        public required string DestinationMacAddress { get; set; }
        
        /// <summary>
        /// Packet content in hexadecimal format.
        /// </summary>
        public required string Content { get; set; }
        
        /// <summary>
        /// Packet capture timestamp in microseconds.
        /// </summary>
        public required ulong TimestampMicroseconds { get; set; }
        
        /// <summary>
        /// IP protocol version (4 or 6), if applicable.
        /// </summary>
        public int? IpVersion { get; set; }
        
        /// <summary>
        /// Source IP address, if applicable.
        /// </summary>
        public string? SourceIpAddress { get; set; }
        
        /// <summary>
        /// Destination IP address, if applicable.
        /// </summary>
        public string? DestinationIpAddress { get; set; }
    }
}
