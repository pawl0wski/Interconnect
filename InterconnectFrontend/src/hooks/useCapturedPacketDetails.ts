import PacketModel from "../models/PacketModel.ts";
import useCapturedPacketDetailsStore from "../store/capturedPacketDetailsStore.ts";
import { usePacketDetailsModalStore } from "../store/modals/modalStores.ts";

const useCapturedPacketDetails = () => {
    const capturedPacketDetailsModalStore = usePacketDetailsModalStore();
    const capturedPacketDetailsStore = useCapturedPacketDetailsStore();

    const showCapturedPacketDetails = (capturedPacket: PacketModel) => {
        capturedPacketDetailsStore.setCapturedPacket(capturedPacket);
        capturedPacketDetailsModalStore.open();
    };

    const closeCapturedPacketDetails = () => {
        capturedPacketDetailsModalStore.close();
        capturedPacketDetailsStore.clearCapturedPacket();
    };

    return { showCapturedPacketDetails, closeCapturedPacketDetails };
};

export default useCapturedPacketDetails;
