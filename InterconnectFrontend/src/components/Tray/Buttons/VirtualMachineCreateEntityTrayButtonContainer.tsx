import VirtualMachineCreateEntityTrayButton from "./VirtualMachineCreateEntityTrayButton.tsx";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";

const VirtualMachineCreateEntityTrayButtonContainer = () => {
    const entityPlacementStore = useEntityPlacementStore();

    const handleCreateVirtualMachine = () => {
        entityPlacementStore.setCurrentEntityType(EntityType.VirtualMachine);
    };

    const isActive = useMemo(
        () =>
            entityPlacementStore.currentEntityType == EntityType.VirtualMachine,
        [entityPlacementStore.currentEntityType],
    );

    return (
        <VirtualMachineCreateEntityTrayButton
            active={isActive}
            onClick={handleCreateVirtualMachine}
        />
    );
};

export default VirtualMachineCreateEntityTrayButtonContainer;
