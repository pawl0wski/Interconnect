import { create } from "zustand/react";
import { hypervisorConnectionClient } from "../api/resourceClient/HypervisorConnectionResourceClient.ts";
import ConnectionInfoModel from "../models/ConnectionInfoModel.ts";

/**
 * State store for managing hypervisor connection information.
 */
interface ConnectionInfoStore {
    /** Cached connection info from the hypervisor */
    connectionInfo: ConnectionInfoModel | null;

    /** Fetches and updates the connection information from the backend */
    updateConnectionInfo: () => Promise<void>;
    /** Clears the cached connection information */
    clearConnectionInfo: () => void;
}

/**
 * Zustand store hook for managing hypervisor connection details.
 * Stores system information such as CPU count, memory, and driver details.
 */
const useConnectionInfoStore = create<ConnectionInfoStore>((set) => ({
    connectionInfo: null,
    updateConnectionInfo: async () => {
        const connectionInfo = (
            await hypervisorConnectionClient.connectionInfo()
        ).data;

        set({
            connectionInfo,
        });
    },
    clearConnectionInfo: () => {
        set({
            connectionInfo: null,
        });
    },
}));

export { useConnectionInfoStore };
