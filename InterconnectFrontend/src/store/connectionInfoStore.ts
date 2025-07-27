import { create } from "zustand/react";
import { hypervisorConnectionClient } from "../api/HypervisorConnectionResourceClient.ts";
import ConnectionInfoModel from "../models/ConnectionInfoModel.ts";

interface ConnectionInfoStore {
    connectionInfo: ConnectionInfoModel | null;

    updateConnectionInfo: () => Promise<void>;
    clearConnectionInfo: () => void;
}

const useConnectionInfoStore = create<ConnectionInfoStore>((set) => ({
    connectionInfo: null,
    updateConnectionInfo: async () => {
        const connectionInfo = (await hypervisorConnectionClient.connectionInfo()).data;

        set({
            connectionInfo
        });
    },
    clearConnectionInfo: () => {
        set({
            connectionInfo: null
        });
    }
}));

export { useConnectionInfoStore };