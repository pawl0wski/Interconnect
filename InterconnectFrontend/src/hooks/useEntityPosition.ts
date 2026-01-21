import { EntityType } from "../models/enums/EntityType.ts";
import { PositionModel } from "../models/PositionModel.ts";
import {
    EntitiesStore,
    useInternetEntitiesStore,
    useVirtualMachineEntitiesStore,
    useVirtualNetworkNodeEntitiesStore,
} from "../store/entitiesStore.ts";
import BaseEntity from "../models/interfaces/BaseEntity.ts";

/**
 * Custom hook that retrieves the position of an entity by its ID and type.
 * Returns default position {x: 0, y: 0} if entity is not found.
 * @param {number} entityId The ID of the entity
 * @param {EntityType} entityType The type of the entity
 * @returns {PositionModel} The position of the entity
 */
const useEntityPosition = (entityId: number, entityType: EntityType) => {
    const position: PositionModel = { x: 0, y: 0 };
    const entitiesStoresMap = {
        [EntityType.VirtualMachine]: useVirtualMachineEntitiesStore(),
        [EntityType.VirtualNetworkNode]: useVirtualNetworkNodeEntitiesStore(),
        [EntityType.Internet]: useInternetEntitiesStore(),
        [EntityType.Network]: undefined,
    };

    const entitiesStoreToUpdatePosition: EntitiesStore<BaseEntity> | undefined =
        entitiesStoresMap[entityType];
    if (!entitiesStoreToUpdatePosition) {
        return position;
    }
    const entity = entitiesStoreToUpdatePosition.getById(entityId);
    if (entity) {
        position.x = entity.x;
        position.y = entity.y;
    }

    return position;
};

export { useEntityPosition };
