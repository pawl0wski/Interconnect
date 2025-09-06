import { ReactNode, useEffect, useState } from "react";
import { useConnectionStore } from "../store/connectionStore.ts";

interface ConnectionStatusProviderProps {
    children: ReactNode;
}

const ConnectionStatusProvider = ({ children }: ConnectionStatusProviderProps) => {
    const updateConnectionStatus = useConnectionStore((state) => state.updateConnectionStatus);
    const [intervalId, setIntervalId] = useState<number | null>(null);

    useEffect(() => {
        if (intervalId) {
            return;
        }

        updateConnectionStatus();

        setIntervalId(setInterval(() => {
            try {
                updateConnectionStatus();
            } catch (e) {
                setIntervalId(null);
                throw e;
            }
        }, 5000));

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
