import { ReactNode, useEffect, useState } from "react";
import { useConnectionStore } from "../store/connectionStore.ts";

interface ConnectionStatusProviderProps {
    children: ReactNode;
}

/**
 * Provider component that monitors the connection status to the backend periodically.
 * Pings the backend every 5 seconds to verify the connection is still active.
 * @param {ConnectionStatusProviderProps} props - Component props
 * @param {ReactNode} props.children - Child components to wrap
 * @returns {JSX.Element} Children with active connection monitoring
 */
const ConnectionStatusProvider = ({
    children,
}: ConnectionStatusProviderProps) => {
    const updateConnectionStatus = useConnectionStore(
        (state) => state.updateConnectionStatus,
    );
    const [intervalId, setIntervalId] = useState<number | null>(null);

    useEffect(() => {
        if (intervalId) {
            return;
        }

        updateConnectionStatus();

        setIntervalId(
            setInterval(() => {
                try {
                    updateConnectionStatus();
                } catch (e) {
                    setIntervalId(null);
                    throw e;
                }
            }, 5000),
        );

        return () => {
            if (!intervalId) {
                return;
            }

            clearInterval(intervalId);
        };
    }, [updateConnectionStatus, intervalId]);

    return <>{children}</>;
};

export default ConnectionStatusProvider;
