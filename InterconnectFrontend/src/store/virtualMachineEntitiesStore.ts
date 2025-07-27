import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { virtualMachineEntityResourceClient } from "../api/VirtualMachineEntityResourceClient.ts";

interface VirtualMachineEntitiesStore {
    entities: VirtualMachineEntityModel[];
    fetchEntities: () => Promise<void>;
    createNewEntity: (name: string, x: number, y: number) => Promise<void>;
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
    createNewEntity: async (name: string, x: number, y: number) => {
        const newEntity = (await virtualMachineEntityResourceClient.createEntity(name, x, y)).data;
        set((state) => ({
            entities: [newEntity, ...state.entities]
        }));
    },
    updateEntityPosition: async (id: number, x: number, y: number) => {
        const foundEntity = get().entities.find((e) => e.id === id);

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