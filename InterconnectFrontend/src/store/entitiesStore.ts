import BaseEntity from "../models/interfaces/BaseEntity.ts";
import { create } from "zustand/react";
import BaseResponse from "../api/responses/BaseResponse.ts";
import UpdateEntityPositionRequest from "../api/requests/UpdateEntityPositionRequest.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

/**
 * Generic state store interface for managing collections of positioned entities.
 */
export interface EntitiesStore<TEntity extends BaseEntity> {
    /** Array of entities of type TEntity */
    entities: TEntity[];
    /** Fetches all entities from backend */
    fetchEntities: () => Promise<void>;
    /** Updates an entity's position on the canvas, optionally persisting to backend */
    updateEntityPosition: (
        id: number,
        x: number,
        y: number,
        finalUpdate?: boolean,
    ) => Promise<void>;
    /** Clears all entities from the store */
    clearEntities: () => void;
    /** Retrieves a specific entity by ID, or null if not found */
    getById: (id: number) => TEntity | null;
    deleteById: (id: number) => Promise<void>;
}

type getEntitiesFunc<TEntity extends BaseEntity> = () => Promise<
    BaseResponse<TEntity[]>
>;
type updateEntityPositionFunc = (
    req: UpdateEntityPositionRequest,
) => Promise<any>;

/**
 * Factory function to create a Zustand store for managing a specific entity type.
 * @template TEntity - The entity model type to manage
 * @param type - The EntityType enum value for this store
 * @param getEntities - Function that fetches entities from backend
 * @param updateEntityPosition - Function that persists position updates to backend
 * @returns A Zustand store hook for managing entities of type TEntity
 */
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
        deleteById: async (id: number) => {
            await entityResourceClient.deleteEntity({ id, type });
            await get().fetchEntities();
        },
    }));

/**
 * Store hook for managing virtual machine entities.
 * Fetches VMs and associates MAC addresses with each VM.
 */
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

/**
 * Store hook for managing virtual network node entities.
 * Fetches network switches and routers (network nodes).
 */
export const useVirtualNetworkNodeEntitiesStore = createEntitiesStore(
    EntityType.VirtualNetworkNode,
    () => entityResourceClient.getAllVirtualNetworkNodeEntities(),
    (req: UpdateEntityPositionRequest) =>
        entityResourceClient.updateEntityPosition(req),
);

/**
 * Store hook for managing internet entity.
 * Fetches and manages the internet gateway entity on the simulation stage.
 */
export const useInternetEntitiesStore = createEntitiesStore(
    EntityType.Internet,
    () => entityResourceClient.getAllInternetEntities(),
    (req: UpdateEntityPositionRequest) =>
        entityResourceClient.updateEntityPosition(req),
);
