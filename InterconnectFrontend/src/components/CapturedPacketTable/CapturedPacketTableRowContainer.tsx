import { useCallback, useMemo } from "react";
import classNames from "classnames";
import CapturedPacketTableRow from "./CapturedPacketTableRow.tsx";
import PacketModel from "../../models/PacketModel.ts";
import styles from "./CapturedPacketTableRow.module.scss";
import useCapturedPacketDetails from "../../hooks/useCapturedPacketDetails.ts";
import useCapturedPacketStore from "../../store/capturedPacketStore.ts";

interface CapturedPacketTableRowContainerProps {
    packet: PacketModel;
}

const CapturedPacketTableRowContainer = ({
    packet,
}: CapturedPacketTableRowContainerProps) => {
    const capturedPacketStore = useCapturedPacketStore();
    const { showCapturedPacketDetails } = useCapturedPacketDetails();

    const handlePacketOver = useCallback(() => {
        capturedPacketStore.setHoveredPacket(packet);
    }, [capturedPacketStore, packet]);

    const handlePacketOut = useCallback(() => {
        if (capturedPacketStore.hoveredPacket !== packet) {
            return;
        }

        capturedPacketStore.setHoveredPacket(null);
    }, [capturedPacketStore, packet]);

    const isHovered = useMemo(
        () => capturedPacketStore.hoveredPacket === packet,
        [capturedPacketStore.hoveredPacket, packet],
    );

    return (
        <CapturedPacketTableRow
            className={classNames(styles["captured-packet-row"], {
                [styles["is-hovered"]]: isHovered,
            })}
            packet={packet}
            onPacketOver={handlePacketOver}
            onPacketOut={handlePacketOut}
            onShowPacketDetails={() => showCapturedPacketDetails(packet)}
        />
    );
};

export default CapturedPacketTableRowContainer;
