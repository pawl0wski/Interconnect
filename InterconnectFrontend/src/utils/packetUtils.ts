import DataLinkLayerPacketType from "../models/enums/DataLinkLayerPacketType.ts";

/**
 * Utility class for network packet-related operations.
 */
const PacketUtils = {
    /**
     * Converts a packet type enumeration value to its human-readable name.
     * @param {DataLinkLayerPacketType} packetType The packet type enum value
     * @returns {string} The human-readable packet type name
     */
    getPacketTypeName: (packetType: DataLinkLayerPacketType) => {
        switch (packetType) {
            case DataLinkLayerPacketType.Arp:
                return "ARP";
            case DataLinkLayerPacketType.Ipv4:
                return "IPv4";
            default:
                return "?";
        }
    },
};

export default PacketUtils;
