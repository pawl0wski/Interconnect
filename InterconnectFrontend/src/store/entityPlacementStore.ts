import { create } from "zustand/react";
import { EntityType } from "../models/enums/EntityType.ts";
import { useVirtualMachineCreateModalStore } from "./modals/virtualMachineCreateModalStore.ts";
import { useVirtualMachineCreateStore } from "./virtualMachineCreateStore.ts";
import useNetworkPlacementStore from "./networkPlacementStore.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";

interface EntityPlacementStore {
    currentEntityType: EntityType | null;
    setCurrentEntityType: (entity: EntityType) => void;
    placeCurrentEntity: (x: number, y: number) => Promise<void>;
    discardPlacingEntity: () => void;
}

const useEntityPlacementStore = create<EntityPlacementStore>()((set, get) => ({
    currentEntityType: null,
    setCurrentEntityType: (type: EntityType) =>
        set({ currentEntityType: type }),
    placeCurrentEntity: async (x: number, y: number): Promise<void> => {
        const { currentEntityType } = get();
        const virtualMachineCreateStore =
            useVirtualMachineCreateStore.getState();
        const virtualMachineCreateModalStore =
            useVirtualMachineCreateModalStore.getState();

        switch (currentEntityType) {
            case EntityType.VirtualMachine:
                virtualMachineCreateStore.update({ x, y });
                virtualMachineCreateModalStore.open();
                break;
            case EntityType.Network:
                const req = useNetworkPlacementStore
                    .getState()
                    .combineAsRequest();
                await virtualNetworkResourceClient.connectEntities(req);
                useNetworkPlacementStore.getState().clear();
                break;
        }
        set({ currentEntityType: null });
    },
    discardPlacingEntity: () => set({ currentEntityType: null }),
}));

export { useEntityPlacementStore };
