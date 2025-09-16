import { ReactNode } from "react";
import ConnectionStatusProvider from "./providers/ConnectionStatusProvider";
import ConnectionInfoProvider from "./providers/ConnectionInfoProvider.tsx";

interface ProviderProps {
    children: ReactNode;
}

const InterconnectProviders = ({ children }: ProviderProps) => (
    <ConnectionStatusProvider>
        <ConnectionInfoProvider>{children}</ConnectionInfoProvider>
    </ConnectionStatusProvider>
);

export default InterconnectProviders;
