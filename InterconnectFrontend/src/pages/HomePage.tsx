import ConnectionInfoModalContainer from "../components/Modals/ConnectionInfoModalContainer.tsx";
import TerminalModalContainer from "../components/Modals/TerminalModelContainer.tsx";
import ErrorModalContainer from "../components/Modals/ErrorModalContainer.tsx";
import Footer from "../components/Footer/Footer.tsx";
import SimulationStage from "../components/SimulationStage/SimulationStage.tsx";
import Tray from "../components/Tray/Tray.tsx";
import VirtualMachineCreateModalContainer from "../components/Modals/VirtualMachineCreateModalContainer.tsx";
import { AppShell } from "@mantine/core";
import Header from "../components/Header/Header.tsx";
import ConnectionOverlay from "../components/Connection/ConnectionOverlay.tsx";

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
            <SimulationStage />
        </AppShell.Main>

        <Tray />
        <Footer />
        <VirtualMachineCreateModalContainer />
        <ConnectionInfoModalContainer />
        <TerminalModalContainer />
        <ErrorModalContainer />
    </AppShell>;
};

export default HomePage;