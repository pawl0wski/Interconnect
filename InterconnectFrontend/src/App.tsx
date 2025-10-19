import { MantineProvider } from "@mantine/core";
import "@mantine/core/styles.css";
import InterconnectProviders from "./InterconnectProviders.tsx";
import InterconnectRoutes from "./InterconnectRoutes.tsx";

const App = () => (
    <MantineProvider>
        <InterconnectProviders>
            <InterconnectRoutes />
        </InterconnectProviders>
    </MantineProvider>
);

export default App;
