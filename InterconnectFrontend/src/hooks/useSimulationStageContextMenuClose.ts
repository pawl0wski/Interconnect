import { useCallback } from "react";
import { useSimulationStageContextMenusStore } from "../store/simulationStageContextMenus.ts";

/**
 * Return type for useSimulationStageContextMenuClose hook.
 */
interface UseSimulationStageContextMenuCloseReturnType {
    /** Function to close the context menu */
    closeContextMenu: () => void;
}

/**
 * Custom hook that provides a function to close the context menu in the simulation stage.
 * @returns {UseSimulationStageContextMenuCloseReturnType} Object with closeContextMenu function
 */
const useSimulationStageContextMenuClose =
    (): UseSimulationStageContextMenuCloseReturnType => {
        const simulationStageContextMenusStore =
            useSimulationStageContextMenusStore();

        const closeContextMenu = useCallback(() => {
            simulationStageContextMenusStore.clearCurrentContextMenu();
        }, [simulationStageContextMenusStore]);

        return { closeContextMenu };
    };

export default useSimulationStageContextMenuClose;
