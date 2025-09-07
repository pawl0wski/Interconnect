import { EntityType } from "../models/enums/EntityType.ts";
import { PositionModel } from "../models/PositionModel.ts";
import { useVirtualMachineEntitiesStore } from "../store/virtualMachineEntitiesStore.ts";
import useVirtualSwitchEntitiesStore from "../store/virtualSwitchEntitiesStore.ts";

const useEntityPosition = (entityId: number, entityType: EntityType) => {
    const position: PositionModel = { x: 0, y: 0 };
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();

    switch (entityType) {
        case EntityType.VirtualMachine:
            const virtualMachine = virtualMachineEntitiesStore.getById(entityId);
            if (virtualMachine) {
                position.x = virtualMachine.x;
                position.y = virtualMachine.y;
            }
            break;
        case EntityType.VirtualSwitch:
            const virtualSwitch = virtualSwitchEntitiesStore.getById(entityId);
            if (virtualSwitch) {
                position.x = virtualSwitch.x;
                position.y = virtualSwitch.y;
            }
            break;
    }

    return position;
};

export { useEntityPosition };