import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";
import VirtualNetworkNodeCreateEntityTrayButton from "./VirtualNetworkNodeCreateEntityTrayButton.tsx";

const VirtualNetworkNodeCreateEntityTrayButtonContainer = () => {
    const entityPlacementStore = useEntityPlacementStore();

    const handleCreateVirtualMachine = () => {
        entityPlacementStore.setCurrentEntityType(EntityType.VirtualNetworkNode);
    };

    const isActive = useMemo(
        () =>
            entityPlacementStore.currentEntityType == EntityType.VirtualNetworkNode,
        [entityPlacementStore.currentEntityType],
    );

    return (
        <VirtualNetworkNodeCreateEntityTrayButton
            active={isActive}
            onClick={handleCreateVirtualMachine}
        />
    );
};

export default VirtualNetworkNodeCreateEntityTrayButtonContainer;
