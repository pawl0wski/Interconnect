import { Table } from "@mantine/core";
import PacketModel from "../../models/PacketModel.ts";
import PacketUtils from "../../utils/packetUtils.ts";

interface CapturedPacketTableRowProps {
    packet: PacketModel;
}

const CapturedPacketTableRow = ({ packet }: CapturedPacketTableRowProps) => (
    <Table.Tr key={packet.ticks}>
        <Table.Td>
            {PacketUtils.getPacketTypeName(packet.dataLinkLayerPacketType)}
        </Table.Td>
        <Table.Td>{packet.sourceMacAddress}</Table.Td>
        <Table.Td>{packet.destinationMacAddress}</Table.Td>
        <Table.Td>{packet.sourceIpAddress}</Table.Td>
        <Table.Td>{packet.destinationIpAddress}</Table.Td>
        <Table.Td></Table.Td>
    </Table.Tr>
);

export default CapturedPacketTableRow;
