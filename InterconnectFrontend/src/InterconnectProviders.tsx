import { ReactNode } from "react";
import ConnectionStatusProvider from "./providers/ConnectionStatusProvider";
import ConnectionInfoProvider from "./providers/ConnectionInfoProvider.tsx";
import InterconnectModalsProvider from "./providers/InterconnectModalsProvider.tsx";

interface ProviderProps {
    children: ReactNode;
}

const InterconnectProviders = ({ children }: ProviderProps) => (
    <ConnectionStatusProvider>
        <ConnectionInfoProvider>
            <InterconnectModalsProvider>{children}</InterconnectModalsProvider>
        </ConnectionInfoProvider>
    </ConnectionStatusProvider>
);

export default InterconnectProviders;
