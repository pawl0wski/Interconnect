import { create } from "zustand/react";
import { PositionModel } from "../models/PositionModel.ts";
import VirtualNetworkNodeEntityModel from "../models/VirtualNetworkNodeEntityModel.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";

interface VirtualNetworkNodeCreateStoreProps {
    name: string | null;
    position: PositionModel | null;
    updateName: (name: string) => void;
    updatePosition: (position: PositionModel) => void;
    create: () => Promise<VirtualNetworkNodeEntityModel>;
}

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
