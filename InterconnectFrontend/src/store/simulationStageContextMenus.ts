import { create } from "zustand/react";
import { PositionModel } from "../models/PositionModel.ts";
import { EntityType } from "../models/enums/EntityType.ts";

/**
 * State store for managing context menu display on the simulation stage.
 * Tracks which entity's context menu is visible and where it appears.
 */
interface SimulationStageContextMenus {
    /** The entity type of the context menu currently visible, or null if hidden */
    currentEntityType: EntityType | null;
    /** The ID of the entity for the current context menu, or null if hidden */
    currentEntityId: number | null;
    /** The cursor position where the context menu should appear */
    currentPosition: PositionModel;
    /** Opens a context menu for the specified entity at the given cursor position */
    setCurrentContextMenu: (
        currentContextMenu: EntityType,
        entityId: number,
        cursorPosition: PositionModel,
    ) => void;
    /** Closes the currently visible context menu */
    clearCurrentContextMenu: () => void;
}

/**
 * Zustand store hook for managing context menu state on simulation stage.
 * Tracks active context menu to prevent multiple simultaneous menus.
 */
const useSimulationStageContextMenusStore =
    create<SimulationStageContextMenus>()((set) => ({
        currentEntityType: null,
        currentEntityId: null,
        currentPosition: { x: 0, y: 0 },
        setCurrentContextMenu: (
            entityType: EntityType,
            entityId: number,
            cursorPosition: PositionModel,
        ) => {
            set({
                currentEntityType: entityType,
                currentEntityId: entityId,
                currentPosition: cursorPosition,
            });
        },
        clearCurrentContextMenu: () =>
            set({ currentEntityType: null, currentEntityId: null }),
    }));

export { useSimulationStageContextMenusStore };
