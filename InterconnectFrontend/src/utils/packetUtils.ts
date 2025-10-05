import DataLinkLayerPacketType from "../models/enums/DataLinkLayerPacketType.ts";

const PacketUtils = {
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
