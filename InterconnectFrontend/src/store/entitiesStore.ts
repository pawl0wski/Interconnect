import BaseEntity from "../models/interfaces/BaseEntity.ts";
import { create } from "zustand/react";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";
import BaseResponse from "../api/responses/BaseResponse.ts";
import { virtualMachineEntityResourceClient } from "../api/resourceClient/VirtualMachineEntityResourceClient.ts";
import UpdateEntityPositionRequest from "../api/requests/UpdateEntityPositionRequest.ts";

export interface EntitiesStore<TEntity extends BaseEntity> {
    entities: TEntity[];
    fetchEntities: () => Promise<void>;
    updateEntityPosition: (id: number, x: number, y: number, finalUpdate?: boolean) => Promise<void>;
    clearEntities: () => void;
    getById: (id: number) => TEntity | null;
}

type getEntitiesFunc<TEntity extends BaseEntity> = () => Promise<BaseResponse<TEntity[]>>
type updateEntityPositionFunc = (req: UpdateEntityPositionRequest) => Promise<any>;

export const createEntitiesStore = <TEntity extends BaseEntity>(
    getEntities: getEntitiesFunc<TEntity>,
    updateEntityPosition: updateEntityPositionFunc
) => (create<EntitiesStore<TEntity>>()((set, get) => ({
    entities: [],
    fetchEntities: async () => {
        const resp = await getEntities();

        set({
            entities: resp.data
        });
    },
    updateEntityPosition: async (id: number, x: number, y: number, finalUpdate?: boolean) => {
        const foundEntity = get().getById(id);

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
            await updateEntityPosition({ id, x, y });
        }
    },
    clearEntities: () => {
        set({ entities: [] });
    },
    getById: (id: number) => get().entities.find(e => e.id === id) ?? null
})));

export const useVirtualSwitchEntitiesStore = createEntitiesStore(
    () => virtualNetworkResourceClient.getVirtualSwitchEntities(),
    (req: UpdateEntityPositionRequest) => virtualNetworkResourceClient.updateVirtualSwitchEntityPosition(req)
);
export const useVirtualMachineEntitiesStore = createEntitiesStore(
    () => virtualMachineEntityResourceClient.getListOfEntities(),
    (req: UpdateEntityPositionRequest) => virtualMachineEntityResourceClient.updateEntityPosition(req)
);
export const useInternetEntitiesStore = createEntitiesStore(
    () => virtualNetworkResourceClient.getInternetEntities(),
    (req: UpdateEntityPositionRequest) => virtualNetworkResourceClient.updateInternetEntityPosition(req)
);