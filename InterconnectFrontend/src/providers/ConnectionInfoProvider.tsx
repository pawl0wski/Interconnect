import { ReactNode, useEffect } from "react";
import { useConnectionInfoStore } from "../store/connectionInfoStore.ts";

interface ConnectionInfoProviderProps {
    children: ReactNode;
}

/**
 * Provider component that fetches and maintains hypervisor connection information.
 * Retrieves system details like CPU count, memory, and driver information on mount.
 * @param {ConnectionInfoProviderProps} props - Component props
 * @param {ReactNode} props.children - Child components to wrap
 * @returns {JSX.Element} Children with available connection info
 */
const ConnectionInfoProvider = ({ children }: ConnectionInfoProviderProps) => {
    const updateConnectionInfo = useConnectionInfoStore(
        (s) => s.updateConnectionInfo,
    );

    useEffect(() => {
        updateConnectionInfo();
    }, [updateConnectionInfo]);

    return <>{children}</>;
};

export default ConnectionInfoProvider;
