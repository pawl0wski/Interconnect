import { ReactNode } from "react";
import ConnectionStatusProvider from "./providers/ConnectionStatusProvider";

interface ProviderProps {
    children: ReactNode;
}

const InterconnectProviders = ({ children }: ProviderProps) => (
    <ConnectionStatusProvider>
        {children}
    </ConnectionStatusProvider>
);

export default InterconnectProviders;