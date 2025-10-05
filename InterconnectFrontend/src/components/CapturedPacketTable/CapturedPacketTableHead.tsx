import { Table } from "@mantine/core";
import { useTranslation } from "react-i18next";

const CapturedPacketTableHead = () => {
    const { t } = useTranslation();

    return (
        <Table.Thead>
            <Table.Tr>
                <Table.Th>{t("type")}</Table.Th>
                <Table.Th>{t("packet.sourceMac")}</Table.Th>
                <Table.Th>{t("packet.destinationMac")}</Table.Th>
                <Table.Th>{t("packet.sourceIp")}</Table.Th>
                <Table.Th>{t("packet.destinationIp")}</Table.Th>
                <Table.Th>{t("actions")}</Table.Th>
            </Table.Tr>
        </Table.Thead>
    );
};

export default CapturedPacketTableHead;
