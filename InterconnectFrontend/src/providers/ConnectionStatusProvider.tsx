import { ReactNode, useEffect } from "react";
import { useConnectionStore } from "../store/connectionStore.ts";

interface ConnectionStatusProviderProps {
    children: ReactNode;
}

const ConnectionStatusProvider = ({ children }: ConnectionStatusProviderProps) => {
    const updateConnectionStatus = useConnectionStore((state) => state.updateConnectionStatus);

    useEffect(() => {
        updateConnectionStatus();

        const id = setInterval(() => {
            updateConnectionStatus();
        }, 5000);

        return () => {
            clearInterval(id);
        };
    }, [updateConnectionStatus]);

    return <>{children}</>;
};

export default ConnectionStatusProvider;
