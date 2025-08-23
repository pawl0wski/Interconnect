import { Flex, Loader, LoadingOverlay, Text } from "@mantine/core";
import { useConnectionStore } from "../../store/connectionStore.ts";
import { ConnectionStatus } from "../../models/enums/ConnectionStatus.ts";
import { useTranslation } from "react-i18next";

const ConnectionOverlay = () => {
    const connectionStatus = useConnectionStore((s) => s.connectionStatus);
    const { t } = useTranslation();

    return <LoadingOverlay
        visible={connectionStatus == ConnectionStatus.Unknown}
        loaderProps={{
            children:
                <Flex direction="column" align="center" justify="space-between" gap="md">
                    <Loader />
                    <Text>{t("connectingToServer")}</Text>
                </Flex>
        }} />;
};

export default ConnectionOverlay;