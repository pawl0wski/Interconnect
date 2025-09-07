import { create } from "zustand/react";
import { EntityType } from "../models/enums/EntityType.ts";
import { useVirtualMachineCreateModalStore, useVirtualSwitchCreateModalStore } from "./modals/modalStores.ts";
import { useVirtualMachineCreateStore } from "./virtualMachineCreateStore.ts";
import useNetworkPlacementStore from "./networkPlacementStore.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";
import useVirtualSwitchCreateStore from "./virtualSwitchCreateStore.ts";
import useNetworkConnectionsStore from "./networkConnectionsStore.ts";

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
        const virtualSwitchCreateModalStore = useVirtualSwitchCreateModalStore.getState();
        const virtualSwitchCreateStore = useVirtualSwitchCreateStore.getState();
        const networkPlacementStore = useNetworkPlacementStore.getState();
        const networkConnectionsStore = useNetworkConnectionsStore.getState();

        switch (currentEntityType) {
            case EntityType.VirtualMachine:
                virtualMachineCreateStore.update({ x, y });
                virtualMachineCreateModalStore.open();
                break;
            case EntityType.Network:
                const req = networkPlacementStore
                    .combineAsRequest();
                await virtualNetworkResourceClient.connectEntities(req);
                networkPlacementStore.clear();
                await networkConnectionsStore.fetch();
                break;
            case EntityType.VirtualSwitch:
                virtualSwitchCreateStore.updatePosition({ x, y });
                virtualSwitchCreateModalStore.open();
                break;
        }
        set({ currentEntityType: null });
    },
    discardPlacingEntity: () => set({ currentEntityType: null })
}));

export { useEntityPlacementStore };
