import { create } from "zustand/react";
import VirtualNetworkConnectionModel from "../models/VirtualNetworkConnectionModel.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";

interface NetworkConnectionsStore {
    networkConnections: VirtualNetworkConnectionModel[];
    fetch: () => Promise<void>;
}

const useNetworkConnectionsStore = create<NetworkConnectionsStore>()((set) => ({
    networkConnections: [],
    fetch: async () => {
        const response = await virtualNetworkResourceClient.getAllConnections();

        set({
            networkConnections: response.data
        });
    }
}));

export default useNetworkConnectionsStore;