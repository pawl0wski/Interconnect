import { useCallback, useMemo } from "react";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import VirtualSwitchContextMenu from "./VirtualSwitchContextMenu.tsx";
import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useVirtualSwitchEntitiesStore } from "../../../store/entitiesStore.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";

const VirtualSwitchContextMenuContainer = () => {
    const simulationStageContextMenusStore = useSimulationStageContextMenusStore();
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const { closeContextMenu } = useSimulationStageContextMenuClose();

    const currentEntity = useMemo(() => {
        const currentEntityId = simulationStageContextMenusStore.currentEntityId;
        if (!currentEntityId) {
            return;
        }

        return virtualSwitchEntitiesStore.getById(currentEntityId);
    }, [simulationStageContextMenusStore.currentEntityId, virtualSwitchEntitiesStore]);
    const { position, visible } = useSimulationStageContextMenuInfo(EntityType.VirtualSwitch);

    const handleStartPlacingVirtualNetwork = useCallback(() => {
        entityPlacementStore.setCurrentEntityType(EntityType.Network);
        networkPlacementStore.setSourceEntity(currentEntity!, EntityType.VirtualSwitch);

        closeContextMenu();
    }, [closeContextMenu, currentEntity, entityPlacementStore, networkPlacementStore]);

    return <VirtualSwitchContextMenu title={currentEntity?.name ?? ""} position={position} isVisible={visible}
                                     onStartPlacingVirtualNetwork={handleStartPlacingVirtualNetwork} />;
};

export default VirtualSwitchContextMenuContainer;