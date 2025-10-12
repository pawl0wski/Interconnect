using Models.Enums;

namespace Models
{
    public class Packet
    {
        public required int Id { get; set; }
        public required DataLinkLayerPacketType DataLinkLayerPacketType { get; set; }
        public required string SourceMacAddress { get; set; }
        public required string DestinationMacAddress { get; set; }
        public int? IpVersion { get; set; }
        public string? SourceIpAddress { get; set; }
        public string? DestinationIpAddress { get; set; }
    }
}
