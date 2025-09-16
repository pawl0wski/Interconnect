import ConnectionInfoModalContainer from "../components/Modals/ConnectionInfoModalContainer.tsx";
import TerminalModalContainer from "../components/Modals/TerminalModalContainer.tsx";
import ErrorModalContainer from "../components/Modals/ErrorModalContainer.tsx";
import Footer from "../components/Footer/Footer.tsx";
import Tray from "../components/Tray/Tray.tsx";
import VirtualMachineCreateModalContainer from "../components/Modals/VirtualMachineCreateModalContainer.tsx";
import { AppShell } from "@mantine/core";
import Header from "../components/Header/Header.tsx";
import ConnectionOverlay from "../components/Connection/ConnectionOverlay.tsx";
import SimulationStageContainer from "../components/SimulationStage/SimulationStageContainer.tsx";
import VirtualSwitchCreateModalContainer from "../components/Modals/VirtualSwitchCreateModalContainer.tsx";

const HomePage = () => {
    const headerHeight = 60;
    const footerHeight = 40;
    const trayHeight = 70;

    return <AppShell
        header={{ height: headerHeight }}
        footer={{ height: footerHeight }}
        padding="md"
    >
        <ConnectionOverlay />
        <AppShell.Header>
            <Header />
        </AppShell.Header>

        <AppShell.Main h={`calc( 100vh - ${headerHeight + footerHeight + trayHeight}px)`}
                       pt={`${headerHeight}px`}
                       pb={`${footerHeight + trayHeight}px`} px={0}>
            <SimulationStageContainer />
        </AppShell.Main>

        <Tray />
        <Footer />
        <VirtualMachineCreateModalContainer />
        <VirtualSwitchCreateModalContainer />
        <ConnectionInfoModalContainer />
        <TerminalModalContainer />
        <ErrorModalContainer />
    </AppShell>;
};

export default HomePage;