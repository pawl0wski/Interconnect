import { ReactNode } from "react";
import ConnectionStatusProvider from "./providers/ConnectionStatusProvider";
import ConnectionInfoProvider from "./providers/ConnectionInfoProvider.tsx";
import InterconnectModalsProvider from "./providers/InterconnectModalsProvider.tsx";

interface ProviderProps {
    children: ReactNode;
}

/**
 * Compound provider component that wraps the application with all necessary context providers.
 * Includes connection status monitoring, connection info context, and modal state management.
 * @param {ProviderProps} props - Component props
 * @param {ReactNode} props.children - Child components to wrap with providers
 * @returns {JSX.Element} Children wrapped with all providers
 */
const InterconnectProviders = ({ children }: ProviderProps) => (
    <ConnectionStatusProvider>
        <ConnectionInfoProvider>
            <InterconnectModalsProvider>{children}</InterconnectModalsProvider>
        </ConnectionInfoProvider>
    </ConnectionStatusProvider>
);

export default InterconnectProviders;
