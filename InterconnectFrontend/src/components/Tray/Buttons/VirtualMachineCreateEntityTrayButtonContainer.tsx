import VirtualMachineCreateEntityTrayButton from "./VirtualMachineCreateEntityTrayButton.tsx";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";

/**
 * Container that binds the VM tray button to the placement store.
 * Toggles VM placement mode and reflects active state.
 * @returns The bound VM tray button component
 */
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
