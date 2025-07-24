import { create } from "zustand/react";
import { ConnectionStatus } from "../models/enums/ConnectionStatus.ts";
import { hypervisorConnectionClient } from "../api/HypervisorConnectionResourceClient.ts";

interface ConnectionStoreState {
    connectionStatus: ConnectionStatus;
    updateConnectionStatus: () => Promise<void>;
}

const useConnectionStore = create<ConnectionStoreState>()((set) => ({
    connectionStatus: ConnectionStatus.Unknown,
    updateConnectionStatus: async () => {
        let connectionStatus: ConnectionStatus;
        try {
            await hypervisorConnectionClient.ping();
            connectionStatus = ConnectionStatus.Alive;
        } catch {
            connectionStatus = ConnectionStatus.Dead;
        }
        set({
            connectionStatus
        });
    }
}));

export { useConnectionStore };
