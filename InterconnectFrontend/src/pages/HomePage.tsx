import Footer from "../components/Footer/Footer.tsx";
import Tray from "../components/Tray/Tray.tsx";
import { AppShell, Flex } from "@mantine/core";
import Header from "../components/Header/Header.tsx";
import ConnectionOverlay from "../components/Connection/ConnectionOverlay.tsx";
import SimulationStageContainer from "../components/SimulationStage/SimulationStageContainer.tsx";
import FullscreenLoader from "../components/FullscreenLoader.tsx";
import CapturedPacketTableContainer from "../components/CapturedPacketTable/CapturedPacketTableContainer.tsx";

const HomePage = () => {
    const headerHeight = 60;
    const footerHeight = 40;
    const trayHeight = 70;

    return (
        <AppShell
            header={{ height: headerHeight }}
            footer={{ height: footerHeight }}
            padding="md"
        >
            <ConnectionOverlay />
            <AppShell.Header>
                <Header />
            </AppShell.Header>

            <AppShell.Main
                h={`calc( 100vh - ${headerHeight + footerHeight + trayHeight}px)`}
                w={`calc( 100vw - 40rem )`}
                pt={`${headerHeight}px`}
                pb={`${footerHeight + trayHeight}px`}
                px={0}
            >
                <Flex w="100vw" h="100%">
                    <SimulationStageContainer />
                    <CapturedPacketTableContainer />
                </Flex>
            </AppShell.Main>

            <Tray />
            <Footer />
            <FullscreenLoader />
        </AppShell>
    );
};

export default HomePage;
