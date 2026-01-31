import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useInternetEntitiesStore } from "../../../store/entitiesStore.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import useNetworkConnectionsStore from "../../../store/networkConnectionsStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";
import { useCallback, useMemo } from "react";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import InternetContextMenu from "./InternetContextMenu.tsx";
import useFullscreenLoader from "../../../hooks/useFullscreenLoader.ts";

const InternetContextMenuContainer = () => {
    const simulationStageContextMenusStore =
        useSimulationStageContextMenusStore();
    const internetEntitiesStore = useInternetEntitiesStore();
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

        return internetEntitiesStore.getById(currentEntityId);
    }, [
        simulationStageContextMenusStore.currentEntityId,
        internetEntitiesStore,
    ]);

    const { position, visible } = useSimulationStageContextMenuInfo(
        EntityType.Internet,
    );

    const handleStartPlacingVirtualNetwork = useCallback(() => {
        entityPlacementStore.setCurrentEntityType(EntityType.Network);
        networkPlacementStore.setSourceEntity(
            currentEntity!,
            EntityType.Internet,
        );

        closeContextMenu();
    }, [
        closeContextMenu,
        currentEntity,
        entityPlacementStore,
        networkPlacementStore,
    ]);

    const handleEntityDelete = useCallback(() => {
        try {
            startLoading();
            internetEntitiesStore.deleteById(currentEntity!.id);
        } finally {
            closeContextMenu();
            stopLoading();
        }
    }, [
        closeContextMenu,
        currentEntity,
        internetEntitiesStore,
        startLoading,
        stopLoading,
    ]);

    const connections =
        currentEntity === null
            ? []
            : networkConnectionsStore.getConnectionsForEntity(
                  currentEntity?.id ?? 0,
                  EntityType.Internet,
              );

    return (
        <InternetContextMenu
            entityId={currentEntity?.id ?? 0}
            position={position}
            isVisible={visible}
            connections={connections}
            onStartPlacingVirtualNetwork={handleStartPlacingVirtualNetwork}
            onDeleteEntity={handleEntityDelete}
        />
    );
};

export default InternetContextMenuContainer;
