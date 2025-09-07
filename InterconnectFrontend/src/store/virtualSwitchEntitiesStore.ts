import VirtualSwitchEntityModel from "../models/VirtualSwitchEntityModel.ts";
import { create } from "zustand/react";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";

interface VirtualSwitchEntitiesStore {
    virtualSwitchEntities: VirtualSwitchEntityModel[];
    fetchEntities: () => Promise<void>;
    updateEntityPosition: (id: number, x: number, y: number, finalUpdate?: boolean) => Promise<void>;
    getById: (id: number) => VirtualSwitchEntityModel | null;
}

const useVirtualSwitchEntitiesStore = create<VirtualSwitchEntitiesStore>()((set, get) => ({
    virtualSwitchEntities: [],
    fetchEntities: async () => {
        const resp = await virtualNetworkResourceClient.getVirtualSwitchEntities();

        set({
            virtualSwitchEntities: resp.data
        });
    },
    updateEntityPosition: async (id: number, x: number, y: number, finalUpdate?: boolean) => {
        const foundEntity = get().getById(id);

        [x, y] = [Math.floor(x), Math.floor(y)];

        if (!foundEntity) {
            return;
        }

        set((state) => {
            const entitiesWithoutFoundEntity = state.virtualSwitchEntities.filter((e) => e.id !== id);

            const updatedEntity = { ...foundEntity, x, y };

            return {
                virtualSwitchEntities: [...entitiesWithoutFoundEntity, updatedEntity]
            };
        });

        if (finalUpdate) {
            await virtualNetworkResourceClient.updateVirtualSwitchEntityPosition({ id, x, y });
        }
    },
    getById: (id: number) => get().virtualSwitchEntities.find(e => e.id === id) ?? null
}));

export default useVirtualSwitchEntitiesStore;