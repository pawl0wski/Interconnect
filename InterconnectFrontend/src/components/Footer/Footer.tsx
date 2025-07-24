import { AppShellFooter, Flex } from "@mantine/core";
import ConnectionStatusIndicatorContainer from "../Connection/ConnectionStatusIndicatorContainer.tsx";

const Footer = () => {
    return <AppShellFooter>
        <Flex align={"center"} justify={"flex-end"} mx="0.5rem">
            <ConnectionStatusIndicatorContainer />
        </Flex>
    </AppShellFooter>;
};

export default Footer;