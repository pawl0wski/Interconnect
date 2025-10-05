import { Table } from "@mantine/core";
import PacketModel from "../../models/PacketModel.ts";
import styles from "./CapturedPacketTable.module.scss";
import CapturedPacketTableRow from "./CapturedPacketTableRow.tsx";
import CapturedPacketTableHead from "./CapturedPacketTableHead.tsx";

interface CapturedPacketListProps {
    packets: PacketModel[];
}

const CapturedPacketTable = ({ packets }: CapturedPacketListProps) => {
    return (
        <div className={styles["captured-packet-table"]}>
            <Table stickyHeader>
                <CapturedPacketTableHead />
                <Table.Tbody>
                    {packets.map((p) => (
                        <CapturedPacketTableRow packet={p} />
                    ))}
                </Table.Tbody>
            </Table>
        </div>
    );
};

export default CapturedPacketTable;
