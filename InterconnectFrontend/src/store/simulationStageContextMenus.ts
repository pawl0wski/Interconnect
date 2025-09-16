import { create } from "zustand/react";
import { PositionModel } from "../models/PositionModel.ts";
import { EntityType } from "../models/enums/EntityType.ts";

interface SimulationStageContextMenus {
    currentEntityType: EntityType | null;
    currentEntityId: number | null;
    currentPosition: PositionModel;
    setCurrentContextMenu: (
        currentContextMenu: EntityType,
        entityId: number,
        cursorPosition: PositionModel,
    ) => void;
    clearCurrentContextMenu: () => void;
}

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
