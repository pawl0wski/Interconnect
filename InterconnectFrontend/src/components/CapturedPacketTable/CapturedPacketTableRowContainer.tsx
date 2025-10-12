import { useCallback, useMemo } from "react";
import PacketModel from "../../models/PacketModel.ts";
import CapturedPacketTableRow from "./CapturedPacketTableRow.tsx";
import useCapturedPacketStore from "../../store/capturedPacketStore.ts";
import styles from "./CapturedPacketTableRow.module.scss";
import classNames from "classnames";

interface CapturedPacketTableRowContainerProps {
    packet: PacketModel;
}

const CapturedPacketTableRowContainer = ({
    packet,
}: CapturedPacketTableRowContainerProps) => {
    const capturedPacketStore = useCapturedPacketStore();

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
        />
    );
};

export default CapturedPacketTableRowContainer;
