import useFullscreenLoaderStore from "../store/fullscreenLoaderStore.ts";
import { Flex, Loader, LoadingOverlay, Text } from "@mantine/core";
import { useTranslation } from "react-i18next";

const FullscreenLoader = () => {
    const fullscreenLoaderStore = useFullscreenLoaderStore();
    const { t } = useTranslation();

    return (
        <LoadingOverlay
            visible={fullscreenLoaderStore.isLoading}
            loaderProps={{
                children: (
                    <Flex
                        direction="column"
                        align="center"
                        justify="space-between"
                        gap="md"
                    >
                        <Loader />
                        <Text>{t("performingActions")}</Text>
                    </Flex>
                ),
            }}
        />
    );
};

export default FullscreenLoader;
