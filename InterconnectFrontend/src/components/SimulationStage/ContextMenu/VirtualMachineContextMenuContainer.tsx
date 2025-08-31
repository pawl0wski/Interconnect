import { useCallback, useMemo } from "react";
import VirtualMachineContextMenu from "./VirtualMachineContextMenu.tsx";
import { useSimulationStageContextMenuInfo } from "../../../hooks/useSimulationStageContextMenuInfo.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useCurrentVirtualMachineModalStore } from "../../../store/modals/currentVirtualMachineModalStore.ts";
import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import { useSimulationStageContextMenusStore } from "../../../store/simulationStageContextMenus.ts";
import { useCurrentVirtualMachineStore } from "../../../store/currentVirtualMachineStore.ts";
import useSimulationStageContextMenuClose from "../../../hooks/useSimulationStageContextMenuClose.ts";

const VirtualMachineContextMenuContainer = () => {
    const simulationStageContextMenusStore = useSimulationStageContextMenusStore();
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();
    const currentVirtualMachineModalStore = useCurrentVirtualMachineModalStore();
    const currentVirtualMachineEntityStore = useCurrentVirtualMachineStore();
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

    return <VirtualMachineContextMenu title={currentEntity?.name ?? ""} position={position} isVisible={visible}
                                      openVirtualMachineConsole={handleOpenVirtualMachineConsole} />;
};

export default VirtualMachineContextMenuContainer;