import { Table } from "@mantine/core";
import PacketModel from "../../models/PacketModel.ts";
import PacketUtils from "../../utils/packetUtils.ts";
import ShowCapturedPacketDetailsButton from "./ShowCapturedPacketDetailsButton.tsx";

interface CapturedPacketTableRowProps {
    className: string;
    packet: PacketModel;
    onPacketOver: (packet: PacketModel) => void;
    onPacketOut: () => void;
    onShowPacketDetails: () => void;
}

const CapturedPacketTableRow = ({
    className,
    packet,
    onPacketOver,
    onPacketOut,
    onShowPacketDetails,
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
            <ShowCapturedPacketDetailsButton onClick={onShowPacketDetails} />
        </Table.Td>
    </Table.Tr>
);

export default CapturedPacketTableRow;
