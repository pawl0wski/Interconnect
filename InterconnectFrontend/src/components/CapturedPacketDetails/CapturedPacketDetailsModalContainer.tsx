import CapturedPacketDetailsModal from "./CapturedPacketDetailsModal.tsx";
import { usePacketDetailsModalStore } from "../../store/modals/modalStores.ts";
import useCapturedPacketDetails from "../../hooks/useCapturedPacketDetails.ts";
import useCapturedPacketDetailsStore from "../../store/capturedPacketDetailsStore.ts";
import { Base64 } from "js-base64";

/**
 * Container that binds the captured packet details modal to stores and derived data.
 * Converts the selected packet content to bytes and passes modal visibility.
 * @returns The packet details modal when a packet is selected, otherwise null
 */
const CapturedPacketDetailsModalContainer = () => {
    const capturedPacketDetailsModalStore = usePacketDetailsModalStore();
    const capturedPacketDetailsStore = useCapturedPacketDetailsStore();
    const { closeCapturedPacketDetails } = useCapturedPacketDetails();
    const { capturedPacket } = capturedPacketDetailsStore;

    const bytes = capturedPacket
        ? Base64.toUint8Array(capturedPacket.content)
        : new Uint8Array();

    return (
        capturedPacket && (
            <CapturedPacketDetailsModal
                bytes={bytes}
                opened={capturedPacketDetailsModalStore.opened}
                packet={capturedPacket}
                onClose={closeCapturedPacketDetails}
            />
        )
    );
};

export default CapturedPacketDetailsModalContainer;
