import { Table } from "@mantine/core";
import PacketModel from "../../models/PacketModel.ts";
import packetUtils from "../../utils/packetUtils.ts";
import { useTranslation } from "react-i18next";

interface CapturedPacketDetailsProps {
    packet: PacketModel;
}

const CapturedPacketDetails = ({ packet }: CapturedPacketDetailsProps) => {
    const {t} = useTranslation();

    return (
        <Table>
            <Table.Thead>
                <Table.Tr>
                    <Table.Th>{t("packetDetails.name")}</Table.Th>
                    <Table.Th>{t("packetDetails.value")}</Table.Th>
                </Table.Tr>
            </Table.Thead>
            <Table.Tbody>
                <Table.Tr>
                    <Table.Td>{t("packetDetails.packetDetails")}</Table.Td>
                    <Table.Td>
                        {packetUtils.getPacketTypeName(
                            packet.dataLinkLayerPacketType,
                        )}
                    </Table.Td>
                </Table.Tr>
                <Table.Tr>
                    <Table.Td>{t("packetDetails.sourceMac")}</Table.Td>
                    <Table.Td>{packet.sourceMacAddress}</Table.Td>
                </Table.Tr>
                <Table.Tr>
                    <Table.Td>{t("packetDetails.destinationMac")}</Table.Td>
                    <Table.Td>{packet.destinationMacAddress}</Table.Td>
                </Table.Tr>
                <Table.Tr>
                    <Table.Td>{t("packetDetails.ipVersion")}</Table.Td>
                    <Table.Td>{packet.ipVersion}</Table.Td>
                </Table.Tr>
                <Table.Tr>
                    <Table.Td>{t("packetDetails.sourceIp")}</Table.Td>
                    <Table.Td>{packet.sourceIpAddress}</Table.Td>
                </Table.Tr>
                <Table.Tr>
                    <Table.Td>{t("packetDetails.destinationIp")}</Table.Td>
                    <Table.Td>{packet.destinationIpAddress}</Table.Td>
                </Table.Tr>
            </Table.Tbody>
        </Table>
    );
};

export default CapturedPacketDetails;
