import { create } from "zustand/react";
import { EntityType } from "../models/enums/EntityType.ts";
import {
    useVirtualMachineCreateModalStore,
    useVirtualNetworkNodeCreateModalStore,
} from "./modals/modalStores.ts";
import { useVirtualMachineCreateStore } from "./virtualMachineCreateStore.ts";
import useNetworkPlacementStore from "./networkPlacementStore.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";
import useVirtualNetworkNodeCreateStore from "./virtualNetworkNodeCreateStore.ts";
import useNetworkConnectionsStore from "./networkConnectionsStore.ts";
import { useInternetEntitiesStore } from "./entitiesStore.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";

/**
 * State store for managing entity placement mode on the simulation stage.
 * Tracks which entity type is being placed and handles the placement workflow.
 */
interface EntityPlacementStore {
    /** The entity type currently being placed, or null if not in placement mode */
    currentEntityType: EntityType | null;
    /** Enters placement mode for the specified entity type */
    setCurrentEntityType: (entity: EntityType) => void;
    /** Places the current entity at the specified coordinates and handles post-placement logic */
    placeCurrentEntity: (x: number, y: number) => Promise<void>;
    /** Cancels entity placement and exits placement mode */
    discardPlacingEntity: () => void;
}

/**
 * Zustand store hook for managing entity placement workflow on simulation stage.
 * Handles different entity placement logic (VMs open modal, networks create connections, etc).
 */
const useEntityPlacementStore = create<EntityPlacementStore>()((set, get) => ({
    currentEntityType: null,
    setCurrentEntityType: (type: EntityType) =>
        set({ currentEntityType: type }),
    placeCurrentEntity: async (x: number, y: number): Promise<void> => {
        [x, y] = [x - 25, y - 100];
        const { currentEntityType } = get();
        const virtualMachineCreateStore =
            useVirtualMachineCreateStore.getState();
        const virtualMachineCreateModalStore =
            useVirtualMachineCreateModalStore.getState();
        const virtualNetworkNodeCreateModalStore =
            useVirtualNetworkNodeCreateModalStore.getState();
        const virtualNetworkNodeCreateStore = useVirtualNetworkNodeCreateStore.getState();
        const networkPlacementStore = useNetworkPlacementStore.getState();
        const networkConnectionsStore = useNetworkConnectionsStore.getState();
        const internetEntitiesStore = useInternetEntitiesStore.getState();

        switch (currentEntityType) {
            case EntityType.VirtualMachine:
                virtualMachineCreateStore.update({ x, y });
                virtualMachineCreateModalStore.open();
                break;
            case EntityType.Network:
                const req = networkPlacementStore.combineAsRequest();
                await virtualNetworkResourceClient.connectEntities(req);
                networkPlacementStore.clear();
                await networkConnectionsStore.fetch();
                break;
            case EntityType.VirtualNetworkNode:
                virtualNetworkNodeCreateStore.updatePosition({ x, y });
                virtualNetworkNodeCreateModalStore.open();
                break;
            case EntityType.Internet:
                const resp =
                    await entityResourceClient.createInternetEntity();
                await internetEntitiesStore.fetchEntities();
                await internetEntitiesStore.updateEntityPosition(
                    resp.data[0].id,
                    x,
                    y,
                    true,
                );
                break;
        }
        set({ currentEntityType: null });
    },
    discardPlacingEntity: () => set({ currentEntityType: null }),
}));

export { useEntityPlacementStore };
