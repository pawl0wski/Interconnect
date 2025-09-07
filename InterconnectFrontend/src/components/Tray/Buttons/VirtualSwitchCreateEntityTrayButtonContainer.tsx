import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";
import VirtualSwitchCreateEntityTrayButton from "./VirtualSwitchCreateEntityTrayButton.tsx";

const VirtualSwitchCreateEntityTrayButtonContainer = () => {
    const entityPlacementStore = useEntityPlacementStore();

    const handleCreateVirtualMachine = () => {
        entityPlacementStore.setCurrentEntityType(EntityType.VirtualSwitch);
    };

    const isActive = useMemo(() => (entityPlacementStore.currentEntityType == EntityType.VirtualSwitch), [entityPlacementStore.currentEntityType]);

    return <VirtualSwitchCreateEntityTrayButton active={isActive} onClick={handleCreateVirtualMachine} />;
};

export default VirtualSwitchCreateEntityTrayButtonContainer;