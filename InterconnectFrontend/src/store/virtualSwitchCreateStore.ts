import { create } from "zustand/react";
import { PositionModel } from "../models/PositionModel.ts";
import VirtualSwitchEntityModel from "../models/VirtualSwitchEntityModel.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";

interface VirtualSwitchCreateStoreProps {
    name: string | null;
    position: PositionModel | null;
    updateName: (name: string) => void;
    updatePosition: (position: PositionModel) => void;
    create: () => Promise<VirtualSwitchEntityModel>;
}

const useVirtualSwitchCreateStore = create<VirtualSwitchCreateStoreProps>()(
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

            const resp = await virtualNetworkResourceClient.createVirtualSwitch(
                { name },
            );
            const entity = resp.data[0];
            await virtualNetworkResourceClient.updateVirtualSwitchEntityPosition(
                {
                    id: entity.id,
                    x: position.x,
                    y: position.y,
                },
            );

            return entity;
        },
    }),
);

export default useVirtualSwitchCreateStore;
