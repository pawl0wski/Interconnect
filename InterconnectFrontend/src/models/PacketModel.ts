import DataLinkLayerPacketType from "./enums/DataLinkLayerPacketType.ts";

interface PacketModel {
    dataLinkLayerPacketType: DataLinkLayerPacketType;
    sourceMacAddress: string;
    destinationMacAddress: string;
    ipVersion: number;
    sourceIpAddress: string;
    destinationIpAddress: string;
    ticks: number;
}

export default PacketModel;
