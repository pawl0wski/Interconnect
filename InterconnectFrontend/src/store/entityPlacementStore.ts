import { EntityType } from "../models/enums/EntityType.ts";
import { create } from "zustand/react";
import { useVirtualMachineCreateModalStore } from "./modals/virtualMachineCreateModalStore.ts";
import { useVirtualMachineCreateStore } from "./virtualMachineCreateStore.ts";

interface EntityPlacementStore {
    currentEntityType: EntityType | null;
    setCurrentEntity: (entity: EntityType) => void;
    placeCurrentEntity: (x: number, y: number) => void;
    discardPlacingEntity: () => void;
}

const useEntityPlacementStore = create<EntityPlacementStore>()((set, get) => ({
    currentEntityType: null,
    setCurrentEntity: (entity: EntityType) => set({ currentEntityType: entity }),
    placeCurrentEntity: (x: number, y: number) => {
        const { currentEntityType } = get();
        const virtualMachineCreateStore = useVirtualMachineCreateStore.getState();
        const virtualMachineCreateModalStore = useVirtualMachineCreateModalStore.getState();

        switch (currentEntityType) {
            case EntityType.VirtualMachine:
                virtualMachineCreateStore.update({ x, y });
                virtualMachineCreateModalStore.open();
                set({ currentEntityType: null });
        }
    },
    discardPlacingEntity: () => set({ currentEntityType: null })
}));

export { useEntityPlacementStore };

