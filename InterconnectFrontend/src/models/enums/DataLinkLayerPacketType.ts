/**
 * Enumeration of data link layer (layer 2) packet types.
 */
enum DataLinkLayerPacketType {
    /** IPv4 packet */
    Ipv4 = 0,
    /** ARP (Address Resolution Protocol) packet */
    Arp = 1,
    /** Unknown packet type */
    Unknown = 3,
}

export default DataLinkLayerPacketType;
