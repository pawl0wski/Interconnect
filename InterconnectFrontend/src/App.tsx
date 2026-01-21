import { MantineProvider } from "@mantine/core";
import "@mantine/core/styles.css";
import InterconnectProviders from "./InterconnectProviders.tsx";
import InterconnectRoutes from "./InterconnectRoutes.tsx";

/**
 * Main application root component. Wraps the entire application with Mantine UI provider
 * and custom context providers for state management and routing.
 * @returns {JSX.Element} The main application component
 */
const App = () => (
    <MantineProvider>
        <InterconnectProviders>
            <InterconnectRoutes />
        </InterconnectProviders>
    </MantineProvider>
);

export default App;
