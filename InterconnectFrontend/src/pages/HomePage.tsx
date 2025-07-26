import { AppShell } from "@mantine/core";
import Footer from "../components/Footer/Footer.tsx";
import ConnectionInfoModalContainer from "../components/Modals/ConnectionInfoModalContainer.tsx";
import SimulationStage from "../components/SimulationStage/SimulationStage.tsx";

const HomePage = () => {
    const headerHeight = 60;
    const footerHeight = 40;

    return <AppShell
        header={{ height: headerHeight }}
        footer={{ height: footerHeight }}
        padding="md"
    >
        <AppShell.Header>
            <div>Interconnect</div>
        </AppShell.Header>

        <AppShell.Main h={`calc( 100vh - ${headerHeight}px - ${footerHeight}px)`} pt={`${headerHeight}px`}
                       pb={`${footerHeight}px`} px={0}>
            <SimulationStage />
        </AppShell.Main>

        <Footer />
        <ConnectionInfoModalContainer />
    </AppShell>;
};

export default HomePage;