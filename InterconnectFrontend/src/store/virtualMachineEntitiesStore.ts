import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { virtualMachineEntityResourceClient } from "../api/resourceClient/VirtualMachineEntityResourceClient.ts";

interface VirtualMachineEntitiesStore {
    entities: VirtualMachineEntityModel[];
    fetchEntities: () => Promise<void>;
    updateEntityPosition: (id: number, x: number, y: number) => Promise<void>;
    clearEntities: () => void;
}

const useVirtualMachineEntitiesStore = create<VirtualMachineEntitiesStore>()((set, get) => ({
    entities: [],
    fetchEntities: async () => {
        const resp = await virtualMachineEntityResourceClient.getListOfEntities();

        set({
            entities: resp.data
        });
    },
    updateEntityPosition: async (id: number, x: number, y: number) => {
        const foundEntity = get().entities.find((e) => e.id === id);

        [x, y] = [Math.floor(x), Math.floor(y)];

        if (!foundEntity) {
            return;
        }

        set((state) => {
            const entitiesWithoutFoundEntity = state.entities.filter((e) => e.id !== id);

            const updatedEntity = { ...foundEntity, x, y };

            return {
                entities: [...entitiesWithoutFoundEntity, updatedEntity]
            };
        });

        await virtualMachineEntityResourceClient.updateEntityPosition(id, x, y);
    },
    clearEntities: () => {
        set({
            entities: []
        });
    }
}));

export { useVirtualMachineEntitiesStore };