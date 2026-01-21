import { useSimulationStageContextMenusStore } from "../store/simulationStageContextMenus.ts";
import { useEffect, useState } from "react";
import { PositionModel } from "../models/PositionModel.ts";
import { EntityType } from "../models/enums/EntityType.ts";

/**
 * Return type for useSimulationStageContextMenuInfo hook.
 */
interface UseSimulationStageContextMenuReturnType {
    /** The position where the context menu should be displayed */
    position: PositionModel;
    /** Whether the context menu should be visible */
    visible: boolean;
}

/**
 * Custom hook that provides context menu visibility and position information.
 * Manages display of context menus for specific entity types in the simulation stage.
 * @param {EntityType} entityType The type of entity for which to show the context menu
 * @returns {UseSimulationStageContextMenuReturnType} Object with position and visibility state
 */
export const useSimulationStageContextMenuInfo = (
    entityType: EntityType,
): UseSimulationStageContextMenuReturnType => {
    const simulationStageContextMenuStore =
        useSimulationStageContextMenusStore();
    const [position, setPosition] = useState<PositionModel>({ x: 0, y: 0 });
    const [visible, setVisible] = useState<boolean>(false);

    useEffect(() => {
        if (simulationStageContextMenuStore.currentEntityType == entityType) {
            setPosition(simulationStageContextMenuStore.currentPosition);
            setVisible(true);
            return;
        }
        setVisible(false);
    }, [
        entityType,
        simulationStageContextMenuStore.currentEntityType,
        simulationStageContextMenuStore.currentPosition,
    ]);

    return { position, visible };
};
