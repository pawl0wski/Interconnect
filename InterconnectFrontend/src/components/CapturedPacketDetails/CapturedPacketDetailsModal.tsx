import { Modal } from "@mantine/core";
import { useTranslation } from "react-i18next";
import PacketModel from "../../models/PacketModel.ts";
import HexViewerContainer from "../HexViewer/HexViewerContainer.tsx";
import CapturedPacketDetails from "./CapturedPacketDetails.tsx";

/**
 * Props for the `CapturedPacketDetailsModal` component.
 */
interface CapturedPacketDetailsModalProps {
    bytes: Uint8Array;
    opened: boolean;
    packet: PacketModel;
    onClose: () => void;
}

/**
 * Modal displaying captured packet metadata and a hex viewer of its payload.
 * @param props Component props
 * @param props.bytes Raw packet bytes rendered in the hex viewer
 * @param props.opened Whether the modal is visible
 * @param props.packet Packet for metadata table
 * @param props.onClose Handler invoked to close the modal
 * @returns An XL modal combining packet details and a hex viewer
 */
const CapturedPacketDetailsModal = ({
    bytes,
    opened,
    packet,
    onClose,
}: CapturedPacketDetailsModalProps) => {
    const { t } = useTranslation();

    return (
        <Modal
            opened={opened}
            onClose={onClose}
            title={t("packetDetails.packetDetails")}
            size="xl"
        >
            <CapturedPacketDetails packet={packet} />
            <br />
            <HexViewerContainer bytes={bytes} />
        </Modal>
    );
};

export default CapturedPacketDetailsModal;
