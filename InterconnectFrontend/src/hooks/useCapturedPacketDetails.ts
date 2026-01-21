import PacketModel from "../models/PacketModel.ts";
import useCapturedPacketDetailsStore from "../store/capturedPacketDetailsStore.ts";
import { usePacketDetailsModalStore } from "../store/modals/modalStores.ts";

/**
 * Custom hook that manages the display and state of captured packet details modal.
 * Provides functions to show/hide the modal and manage the displayed packet data.
 * @returns {Object} Object with showCapturedPacketDetails and closeCapturedPacketDetails functions
 */
const useCapturedPacketDetails = () => {
    const capturedPacketDetailsModalStore = usePacketDetailsModalStore();
    const capturedPacketDetailsStore = useCapturedPacketDetailsStore();

    /**
     * Displays the captured packet details modal with the given packet.
     * @param {PacketModel} capturedPacket The packet to display
     */
    const showCapturedPacketDetails = (capturedPacket: PacketModel) => {
        capturedPacketDetailsStore.setCapturedPacket(capturedPacket);
        capturedPacketDetailsModalStore.open();
    };

    /**
     * Closes the captured packet details modal and clears the stored packet.
     */
    const closeCapturedPacketDetails = () => {
        capturedPacketDetailsModalStore.close();
        capturedPacketDetailsStore.clearCapturedPacket();
    };

    return { showCapturedPacketDetails, closeCapturedPacketDetails };
};

export default useCapturedPacketDetails;
