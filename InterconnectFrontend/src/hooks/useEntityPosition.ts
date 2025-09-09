import { EntityType } from "../models/enums/EntityType.ts";
import { PositionModel } from "../models/PositionModel.ts";
import {
    EntitiesStore,
    useInternetEntitiesStore,
    useVirtualMachineEntitiesStore,
    useVirtualSwitchEntitiesStore
} from "../store/entitiesStore.ts";
import BaseEntity from "../models/interfaces/BaseEntity.ts";

const useEntityPosition = (entityId: number, entityType: EntityType) => {
    const position: PositionModel = { x: 0, y: 0 };
    const entitiesStoresMap = new Map([
        [EntityType.VirtualMachine, useVirtualMachineEntitiesStore()],
        [EntityType.VirtualSwitch, useVirtualSwitchEntitiesStore()],
        [EntityType.Internet, useInternetEntitiesStore()]
    ]);

    const entitiesStoreToUpdatePosition: EntitiesStore<BaseEntity> | undefined = entitiesStoresMap.get(entityType);
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