import { useCallback } from "react";
import { useSimulationStageContextMenusStore } from "../store/simulationStageContextMenus.ts";

interface UseSimulationStageContextMenuCloseReturnType {
    closeContextMenu: () => void;
}

const useSimulationStageContextMenuClose = (): UseSimulationStageContextMenuCloseReturnType => {
    const simulationStageContextMenusStore = useSimulationStageContextMenusStore();

    const closeContextMenu = useCallback(() => {
        simulationStageContextMenusStore.clearCurrentContextMenu();
    }, [simulationStageContextMenusStore]);

    return { closeContextMenu };
};

export default useSimulationStageContextMenuClose;