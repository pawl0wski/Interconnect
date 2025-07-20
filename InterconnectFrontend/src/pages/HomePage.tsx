import { AppShell, Burger } from "@mantine/core";

const HomePage = () => {
    // const connectionStatus = useConnectionStore((state) => state.connectionStatus);

    return <AppShell
        header={{ height: 60 }}
        navbar={{
            width: 300,
            breakpoint: "sm",
        }}
        padding="md"
    >
        <AppShell.Header>
            <Burger />
            <div>Logo</div>
        </AppShell.Header>

        <AppShell.Navbar p="md">Navbar</AppShell.Navbar>

        <AppShell.Main>Main</AppShell.Main>
    </AppShell>;
};

export default HomePage;