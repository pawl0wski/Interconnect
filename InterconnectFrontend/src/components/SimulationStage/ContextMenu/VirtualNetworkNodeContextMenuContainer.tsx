import { useCallback, useMemo } from "react";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import VirtualNetworkNodeContextMenu from "./VirtualNetworkNodeContextMenu.tsx";
import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useVirtualNetworkNodeEntitiesStore } from "../../../store/entitiesStore.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";
import useNetworkConnectionsStore from "../../../store/networkConnectionsStore.ts";
import useFullscreenLoader from "../../../hooks/useFullscreenLoader.ts";

const VirtualNetworkNodeContextMenuContainer = () => {
    const simulationStageContextMenusStore =
        useSimulationStageContextMenusStore();
    const virtualNetworkNodeEntitiesStore =
        useVirtualNetworkNodeEntitiesStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const networkConnectionsStore = useNetworkConnectionsStore();
    const { closeContextMenu } = useSimulationStageContextMenuClose();
    const { startLoading, stopLoading } = useFullscreenLoader();

    const currentEntity = useMemo(() => {
        const currentEntityId =
            simulationStageContextMenusStore.currentEntityId;
        if (!currentEntityId) {
            return;
        }

        return virtualNetworkNodeEntitiesStore.getById(currentEntityId);
    }, [
        simulationStageContextMenusStore.currentEntityId,
        virtualNetworkNodeEntitiesStore,
    ]);
    const { position, visible } = useSimulationStageContextMenuInfo(
        EntityType.VirtualNetworkNode,
    );

    const handleStartPlacingVirtualNetwork = useCallback(() => {
        entityPlacementStore.setCurrentEntityType(EntityType.Network);
        networkPlacementStore.setSourceEntity(
            currentEntity!,
            EntityType.VirtualNetworkNode,
        );

        closeContextMenu();
    }, [
        closeContextMenu,
        currentEntity,
        entityPlacementStore,
        networkPlacementStore,
    ]);

    const handleDeleteEntity = useCallback(async () => {
        try {
            startLoading();
            await virtualNetworkNodeEntitiesStore.deleteById(currentEntity!.id);
        } finally {
            closeContextMenu();
            stopLoading();
        }
    }, [
        currentEntity,
        startLoading,
        stopLoading,
        virtualNetworkNodeEntitiesStore,
    ]);

    const connections =
        currentEntity === null
            ? []
            : networkConnectionsStore.getConnectionsForEntity(
                  currentEntity?.id ?? 0,
                  EntityType.VirtualNetworkNode,
              );

    return (
        <VirtualNetworkNodeContextMenu
            entityId={currentEntity?.id ?? 0}
            title={currentEntity?.name ?? ""}
            position={position}
            isVisible={visible}
            connections={connections}
            onStartPlacingVirtualNetwork={handleStartPlacingVirtualNetwork}
            onDeleteEntity={handleDeleteEntity}
        />
    );
};

export default VirtualNetworkNodeContextMenuContainer;
