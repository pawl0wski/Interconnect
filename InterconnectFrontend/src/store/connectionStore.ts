import { create } from "zustand/react";
import { ConnectionStatus } from "../models/enums/ConnectionStatus.ts";
import { connectionStatusHubClient } from "../api/hubClient/ConnectionStatusHubClient.ts";

interface ConnectionStoreState {
    connectionStatus: ConnectionStatus;
    updateConnectionStatus: () => Promise<void>;
}

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
