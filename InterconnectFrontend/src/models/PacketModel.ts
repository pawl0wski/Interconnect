import DataLinkLayerPacketType from "./enums/DataLinkLayerPacketType.ts";

/**
 * Represents a captured network packet with layer 2 and layer 3 information.
 */
interface PacketModel {
    /** Unique identifier for the captured packet */
    id: number;
    /** Type of data link layer (layer 2) packet */
    dataLinkLayerPacketType: DataLinkLayerPacketType;
    /** Source MAC address */
    sourceMacAddress: string;
    /** Destination MAC address */
    destinationMacAddress: string;
    /** Raw packet content encoded as string */
    content: string;
    /** IP version (4 or 6) */
    ipVersion: number;
    /** Source IP address */
    sourceIpAddress: string;
    /** Destination IP address */
    destinationIpAddress: string;
    /** Timestamp of packet capture in microseconds */
    timestampMicroseconds: number;
}

export default PacketModel;
