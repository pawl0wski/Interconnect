import { useCallback, useMemo } from "react";
import VirtualMachineContextMenu from "./VirtualMachineContextMenu.tsx";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useCurrentVirtualMachineModalStore } from "../../../store/modals/modalStores.ts";
import { useVirtualMachineEntitiesStore } from "../../../store/entitiesStore.ts";
import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useCurrentVirtualMachineStore } from "../../../store/currentVirtualMachineStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import useNetworkConnectionsStore from "../../../store/networkConnectionsStore.ts";

const VirtualMachineContextMenuContainer = () => {
    const simulationStageContextMenusStore =
        useSimulationStageContextMenusStore();
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();
    const currentVirtualMachineModalStore =
        useCurrentVirtualMachineModalStore();
    const currentVirtualMachineEntityStore = useCurrentVirtualMachineStore();
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

        return virtualMachineEntitiesStore.getById(currentEntityId);
    }, [
        simulationStageContextMenusStore.currentEntityId,
        virtualMachineEntitiesStore,
    ]);
    const { position, visible } = useSimulationStageContextMenuInfo(
        EntityType.VirtualMachine,
    );

    const handleOpenVirtualMachineConsole = useCallback(() => {
        if (!currentEntity) {
            return;
        }

        currentVirtualMachineEntityStore.setCurrentEntity(currentEntity);
        currentVirtualMachineModalStore.open();
        closeContextMenu();
    }, [
        closeContextMenu,
        currentEntity,
        currentVirtualMachineEntityStore,
        currentVirtualMachineModalStore,
    ]);

    const handleStartPlacingVirtualNetwork = useCallback(() => {
        entityPlacementStore.setCurrentEntityType(EntityType.Network);
        networkPlacementStore.setSourceEntity(
            currentEntity!,
            EntityType.VirtualMachine,
        );

        closeContextMenu();
    }, [
        closeContextMenu,
        currentEntity,
        entityPlacementStore,
        networkPlacementStore,
    ]);

    const entityConnections = networkConnectionsStore.getConnectionsForEntity(
        currentEntity?.id ?? 0,
        EntityType.VirtualMachine,
    );

    return (
        <VirtualMachineContextMenu
            entityId={currentEntity?.id ?? 0}
            title={currentEntity?.name ?? ""}
            position={position}
            isVisible={visible}
            connections={entityConnections}
            onOpenVirtualMachineConsole={handleOpenVirtualMachineConsole}
            onStartPlacingVirtualNetwork={handleStartPlacingVirtualNetwork}
        />
    );
};

export default VirtualMachineContextMenuContainer;
