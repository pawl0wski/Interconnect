import useFullscreenLoaderStore from "../store/fullscreenLoaderStore.ts";
import { Flex, Loader, LoadingOverlay, Text } from "@mantine/core";
import { useTranslation } from "react-i18next";

/**
 * Fullscreen overlay loader displayed during long-running actions.
 * Reads visibility from the `fullscreenLoaderStore` and shows a spinner with text.
 * @returns Loading overlay element that covers the app content when active
 */
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
