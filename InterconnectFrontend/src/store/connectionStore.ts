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
        const status = await hypervisorConnectionClient.connectionStatus();
        set({
            connectionStatus: status.data ? ConnectionStatus.Alive : ConnectionStatus.Dead,
        });
    },
}));

export { useConnectionStore };
