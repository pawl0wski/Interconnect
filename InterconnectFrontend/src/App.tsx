import "@mantine/core/styles.css";
import InterconnectProviders from "./InterconnectProviders.tsx";
import InterconnectRoutes from "./InterconnectRoutes.tsx";
import { MantineProvider } from "@mantine/core";

const App = () => (
    <MantineProvider>
        <InterconnectProviders>
            <InterconnectRoutes />
        </InterconnectProviders>
    </MantineProvider>
);

export default App;
