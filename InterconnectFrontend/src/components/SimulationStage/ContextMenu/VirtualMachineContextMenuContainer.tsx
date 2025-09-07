import { useCallback, useMemo } from "react";
import VirtualMachineContextMenu from "./VirtualMachineContextMenu.tsx";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useCurrentVirtualMachineModalStore } from "../../../store/modals/modalStores.ts";
import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useCurrentVirtualMachineStore } from "../../../store/currentVirtualMachineStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";

const VirtualMachineContextMenuContainer = () => {
    const simulationStageContextMenusStore = useSimulationStageContextMenusStore();
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();
    const currentVirtualMachineModalStore = useCurrentVirtualMachineModalStore();
    const currentVirtualMachineEntityStore = useCurrentVirtualMachineStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const { closeContextMenu } = useSimulationStageContextMenuClose();

    const currentEntity = useMemo(() => {
        const currentEntityId = simulationStageContextMenusStore.currentEntityId;
        if (!currentEntityId) {
            return;
        }

        return virtualMachineEntitiesStore.getById(currentEntityId);
    }, [simulationStageContextMenusStore.currentEntityId, virtualMachineEntitiesStore]);
    const { position, visible } = useSimulationStageContextMenuInfo(EntityType.VirtualMachine);


    const handleOpenVirtualMachineConsole = useCallback(() => {
        if (!currentEntity) {
            return;
        }

        currentVirtualMachineEntityStore.setCurrentEntity(currentEntity);
        currentVirtualMachineModalStore.open();
        closeContextMenu();
    }, [closeContextMenu, currentEntity, currentVirtualMachineEntityStore, currentVirtualMachineModalStore]);

    const handleStartPlacingVirtualNetwork = useCallback(() => {
        entityPlacementStore.setCurrentEntityType(EntityType.Network);
        networkPlacementStore.setSourceEntity(currentEntity!, EntityType.VirtualMachine);

        closeContextMenu();
    }, [closeContextMenu, currentEntity, entityPlacementStore, networkPlacementStore]);

    return <VirtualMachineContextMenu title={currentEntity?.name ?? ""} position={position} isVisible={visible}
                                      onOpenVirtualMachineConsole={handleOpenVirtualMachineConsole}
                                      onStartPlacingVirtualNetwork={handleStartPlacingVirtualNetwork} />;
};

export default VirtualMachineContextMenuContainer;