import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { virtualMachineEntityResourceClient } from "../api/resourceClient/VirtualMachineEntityResourceClient.ts";

interface VirtualMachineEntitiesStore {
    entities: VirtualMachineEntityModel[];
    fetchEntities: () => Promise<void>;
    updateEntityPosition: (id: number, x: number, y: number, finalUpdate?: boolean) => Promise<void>;
    clearEntities: () => void;
    getById: (id: number) => VirtualMachineEntityModel;
    getByVmUuid: (uuid: string) => VirtualMachineEntityModel | undefined;
}

const useVirtualMachineEntitiesStore = create<VirtualMachineEntitiesStore>()((set, get) => ({
    entities: [],
    fetchEntities: async () => {
        const resp = await virtualMachineEntityResourceClient.getListOfEntities();

        set({
            entities: resp.data
        });
    },
    updateEntityPosition: async (id: number, x: number, y: number, finalUpdate: boolean = false) => {
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

        if (finalUpdate) {
            await virtualMachineEntityResourceClient.updateEntityPosition(id, x, y);
        }
    },
    clearEntities: () => {
        set({
            entities: []
        });
    },
    getById: (id: number) => get().entities.find((e) => e.id === id)!,
    getByVmUuid: (uuid: string) => get().entities.find((e) => e.vmUuid === uuid)
}));

export { useVirtualMachineEntitiesStore };