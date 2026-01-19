import BaseEntity from "../models/interfaces/BaseEntity.ts";
import { create } from "zustand/react";
import BaseResponse from "../api/responses/BaseResponse.ts";
import UpdateEntityPositionRequest from "../api/requests/UpdateEntityPositionRequest.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

export interface EntitiesStore<TEntity extends BaseEntity> {
    entities: TEntity[];
    fetchEntities: () => Promise<void>;
    updateEntityPosition: (
        id: number,
        x: number,
        y: number,
        finalUpdate?: boolean,
    ) => Promise<void>;
    clearEntities: () => void;
    getById: (id: number) => TEntity | null;
}

type getEntitiesFunc<TEntity extends BaseEntity> = () => Promise<
    BaseResponse<TEntity[]>
>;
type updateEntityPositionFunc = (
    req: UpdateEntityPositionRequest,
) => Promise<any>;

export const createEntitiesStore = <TEntity extends BaseEntity>(
    type: EntityType,
    getEntities: getEntitiesFunc<TEntity>,
    updateEntityPosition: updateEntityPositionFunc,
) =>
    create<EntitiesStore<TEntity>>()((set, get) => ({
        entities: [],
        fetchEntities: async () => {
            const resp = await getEntities();

            set({
                entities: resp.data,
            });
        },
        updateEntityPosition: async (
            id: number,
            x: number,
            y: number,
            finalUpdate?: boolean,
        ) => {
            const foundEntity = get().getById(id);

            [x, y] = [Math.floor(x), Math.floor(y)];

            if (!foundEntity) {
                return;
            }

            set((state) => {
                const entitiesWithoutFoundEntity = state.entities.filter(
                    (e) => e.id !== id,
                );

                const updatedEntity = { ...foundEntity, x, y };

                return {
                    entities: [...entitiesWithoutFoundEntity, updatedEntity],
                };
            });

            if (finalUpdate) {
                await updateEntityPosition({ id, type, x, y });
            }
        },
        clearEntities: () => {
            set({ entities: [] });
        },
        getById: (id: number) =>
            get().entities.find((e) => e.id === id) ?? null,
    }));

export const useVirtualMachineEntitiesStore =
    createEntitiesStore<VirtualMachineEntityModel>(
        EntityType.VirtualMachine,
        async () => {
            const resp =
                await entityResourceClient.getAllVirtualMachineEntities();

            const vms = resp.data.virtualMachineEntities;
            const macs = resp.data.macAddressEntities;

            const entities: VirtualMachineEntityModel[] = vms.map((vm) => {
                const macEntry = macs.find(
                    (m) => m.virtualMachineEntityId === vm.id,
                );

                return {
                    ...vm,
                    macAddresses: macEntry?.macAddresses ?? [],
                };
            });

            return {
                ...resp,
                data: entities,
            };
        },
        (req: UpdateEntityPositionRequest) =>
            entityResourceClient.updateEntityPosition(req),
    );
export const useVirtualNetworkNodeEntitiesStore = createEntitiesStore(
    EntityType.VirtualNetworkNode,
    () => entityResourceClient.getAllVirtualNetworkNodeEntities(),
    (req: UpdateEntityPositionRequest) =>
        entityResourceClient.updateEntityPosition(req),
);
export const useInternetEntitiesStore = createEntitiesStore(
    EntityType.Internet,
    () => entityResourceClient.getAllInternetEntities(),
    (req: UpdateEntityPositionRequest) =>
        entityResourceClient.updateEntityPosition(req),
);
