import { create } from "zustand/react";
import VirtualNetworkEntityConnectionModel from "../models/VirtualNetworkEntityConnectionModel.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";

interface VirtualNetworkEntitiesStore {
    virtualNetworkEntities: VirtualNetworkEntityConnectionModel[];
    fetchVirtualNetworkEntities: () => Promise<void>;
}

const useVirtualNetworkEntitiesStore = create<VirtualNetworkEntitiesStore>()((set) => ({
    virtualNetworkEntities: [],
    fetchVirtualNetworkEntities: async () => {
        const response = await virtualNetworkResourceClient.getAllConnections();

        set({
            virtualNetworkEntities: response.data
        });
    }
}));

export default useVirtualNetworkEntitiesStore;