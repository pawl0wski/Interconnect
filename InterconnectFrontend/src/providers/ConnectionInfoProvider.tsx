import { ReactNode, useEffect } from "react";
import { useConnectionInfoStore } from "../store/connectionInfoStore.ts";

interface ConnectionInfoProviderProps {
    children: ReactNode;
}

const ConnectionInfoProvider = ({ children }: ConnectionInfoProviderProps) => {
    const updateConnectionInfo = useConnectionInfoStore(s => s.updateConnectionInfo);

    useEffect(() => {
        updateConnectionInfo();
    }, [updateConnectionInfo]);

    return <>{children}</>;
};

export default ConnectionInfoProvider;