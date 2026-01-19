import DataLinkLayerPacketType from "./enums/DataLinkLayerPacketType.ts";

interface PacketModel {
    id: number;
    dataLinkLayerPacketType: DataLinkLayerPacketType;
    sourceMacAddress: string;
    destinationMacAddress: string;
    content: string;
    ipVersion: number;
    sourceIpAddress: string;
    destinationIpAddress: string;
    timestampMicroseconds: number;
}

export default PacketModel;
