import { Modal } from "@mantine/core";
import { useTranslation } from "react-i18next";
import PacketModel from "../../models/PacketModel.ts";
import HexViewerContainer from "../HexViewer/HexViewerContainer.tsx";
import CapturedPacketDetails from "./CapturedPacketDetails.tsx";

interface CapturedPacketDetailsModalProps {
    bytes: Uint8Array;
    opened: boolean;
    packet: PacketModel;
    onClose: () => void;
}

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
