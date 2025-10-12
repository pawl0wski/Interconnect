import { Button, Table } from "@mantine/core";
import PacketModel from "../../models/PacketModel.ts";
import PacketUtils from "../../utils/packetUtils.ts";
import { MdOpenInNew } from "react-icons/md";

interface CapturedPacketTableRowProps {
    className: string;
    packet: PacketModel;
    onPacketOver: (packet: PacketModel) => void;
    onPacketOut: () => void;
}

const CapturedPacketTableRow = ({
    className,
    packet,
    onPacketOver,
    onPacketOut,
}: CapturedPacketTableRowProps) => (
    <Table.Tr
        className={className}
        onMouseOver={() => onPacketOver(packet)}
        onMouseOut={onPacketOut}
    >
        <Table.Td>
            {PacketUtils.getPacketTypeName(packet.dataLinkLayerPacketType)}
        </Table.Td>
        <Table.Td>{packet.sourceMacAddress}</Table.Td>
        <Table.Td>{packet.destinationMacAddress}</Table.Td>
        <Table.Td>{packet.sourceIpAddress}</Table.Td>
        <Table.Td>{packet.destinationIpAddress}</Table.Td>
        <Table.Td>
            <Button variant="transparent" size="md">
                <MdOpenInNew />
            </Button>
        </Table.Td>
    </Table.Tr>
);

export default CapturedPacketTableRow;
