import { create } from "zustand/react";
import { ConnectionStatus } from "../models/enums/ConnectionStatus.ts";
import { connectionStatusHubClient } from "../api/hubClient/ConnectionStatusHubClient.ts";

/**
 * State store for managing the connection status to the backend.
 */
interface ConnectionStoreState {
    /** Current connection status (Alive, Dead, or Unknown) */
    connectionStatus: ConnectionStatus;
    /** Updates the connection status by pinging the backend */
    updateConnectionStatus: () => Promise<void>;
}

/**
 * Zustand store hook for monitoring backend connection status.
 * Uses the ConnectionStatusHub to ping and verify connection.
 */
const useConnectionStore = create<ConnectionStoreState>()((set) => ({
    connectionStatus: ConnectionStatus.Unknown,
    updateConnectionStatus: async () => {
        let connectionStatus: ConnectionStatus;
        try {
            const resp = await connectionStatusHubClient.ping();
            if (resp.data !== "Pong") {
                connectionStatus = ConnectionStatus.Dead;
            } else {
                connectionStatus = ConnectionStatus.Alive;
            }
        } catch {
            connectionStatus = ConnectionStatus.Dead;
        }
        set({
            connectionStatus,
        });
    },
}));

export { useConnectionStore };
