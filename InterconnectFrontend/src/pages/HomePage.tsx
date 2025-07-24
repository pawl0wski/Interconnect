import { AppShell } from "@mantine/core";
import Footer from "../components/Footer/Footer.tsx";
import ConnectionInfoModalContainer from "../components/Modals/ConnectionInfoModalContainer.tsx";

const HomePage = () => {
    return <AppShell
        header={{ height: 60 }}
        padding="md"
    >
        <AppShell.Header>
            <div>Interconnect</div>
        </AppShell.Header>

        <AppShell.Main>Main</AppShell.Main>

        <Footer />
        <ConnectionInfoModalContainer />
    </AppShell>;
};

export default HomePage;