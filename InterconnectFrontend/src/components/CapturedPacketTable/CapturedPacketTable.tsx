import { Table } from "@mantine/core";
import PacketModel from "../../models/PacketModel.ts";
import styles from "./CapturedPacketTable.module.scss";
import CapturedPacketTableHead from "./CapturedPacketTableHead.tsx";
import CapturedPacketTableRowContainer from "./CapturedPacketTableRowContainer.tsx";

interface CapturedPacketListProps {
    packets: PacketModel[];
}

const CapturedPacketTable = ({ packets }: CapturedPacketListProps) => (
    <div className={styles["captured-packet-table"]}>
        <Table stickyHeader>
            <CapturedPacketTableHead />
            <Table.Tbody>
                {packets.map((p) => (
                    <CapturedPacketTableRowContainer packet={p} key={p.id} />
                ))}
            </Table.Tbody>
        </Table>
    </div>
);

export default CapturedPacketTable;
