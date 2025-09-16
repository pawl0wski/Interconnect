import { useCallback, useMemo } from "react";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import VirtualSwitchContextMenu from "./VirtualSwitchContextMenu.tsx";
import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useVirtualSwitchEntitiesStore } from "../../../store/entitiesStore.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";
import useNetworkConnectionsStore from "../../../store/networkConnectionsStore.ts";

const VirtualSwitchContextMenuContainer = () => {
    const simulationStageContextMenusStore =
        useSimulationStageContextMenusStore();
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const networkConnectionsStore = useNetworkConnectionsStore();
    const { closeContextMenu } = useSimulationStageContextMenuClose();

    const currentEntity = useMemo(() => {
        const currentEntityId =
            simulationStageContextMenusStore.currentEntityId;
        if (!currentEntityId) {
            return;
        }

        return virtualSwitchEntitiesStore.getById(currentEntityId);
    }, [
        simulationStageContextMenusStore.currentEntityId,
        virtualSwitchEntitiesStore,
    ]);
    const { position, visible } = useSimulationStageContextMenuInfo(
        EntityType.VirtualSwitch,
    );

    const handleStartPlacingVirtualNetwork = useCallback(() => {
        entityPlacementStore.setCurrentEntityType(EntityType.Network);
        networkPlacementStore.setSourceEntity(
            currentEntity!,
            EntityType.VirtualSwitch,
        );

        closeContextMenu();
    }, [
        closeContextMenu,
        currentEntity,
        entityPlacementStore,
        networkPlacementStore,
    ]);

    const connections =
        currentEntity === null
            ? []
            : networkConnectionsStore.getConnectionsForEntity(
                  currentEntity?.id ?? 0,
                  EntityType.VirtualSwitch,
              );

    return (
        <VirtualSwitchContextMenu
            entityId={currentEntity?.id ?? 0}
            title={currentEntity?.name ?? ""}
            position={position}
            isVisible={visible}
            connections={connections}
            onStartPlacingVirtualNetwork={handleStartPlacingVirtualNetwork}
        />
    );
};

export default VirtualSwitchContextMenuContainer;
