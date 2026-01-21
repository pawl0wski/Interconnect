import { create } from "zustand/react";
import { PositionModel } from "../models/PositionModel.ts";
import VirtualNetworkNodeEntityModel from "../models/VirtualNetworkNodeEntityModel.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";

/**
 * State store for managing virtual network node creation form data and submission.
 */
interface VirtualNetworkNodeCreateStoreProps {
    /** Network node name/display name, or null if not set */
    name: string | null;
    /** Position on simulation stage canvas, or null if not set */
    position: PositionModel | null;
    /** Updates the network node name */
    updateName: (name: string) => void;
    /** Updates the network node position */
    updatePosition: (position: PositionModel) => void;
    /** Creates the network node on backend and returns the created entity */
    create: () => Promise<VirtualNetworkNodeEntityModel>;
}

/**
 * Zustand store hook for managing virtual network node creation workflow.
 * Tracks form state and handles network node creation API calls.
 */
const useVirtualNetworkNodeCreateStore = create<VirtualNetworkNodeCreateStoreProps>()(
    (set, get) => ({
        name: null,
        position: null,
        updateName: (name: string) => {
            set({ name });
        },
        updatePosition: (position: PositionModel) => {
            set({ position });
        },
        create: async () => {
            const { name, position } = get();
            if (!name || !position) {
                throw new Error("Name or position is not set");
            }

            const resp = await entityResourceClient.createVirtualNetworkNodeEntity({
                name,
            });
            const entity = resp.data[0];
            await entityResourceClient.updateEntityPosition({
                id: entity.id,
                type: EntityType.VirtualNetworkNode,
                x: position.x,
                y: position.y,
            });

            return entity;
        },
    }),
);

export default useVirtualNetworkNodeCreateStore;
