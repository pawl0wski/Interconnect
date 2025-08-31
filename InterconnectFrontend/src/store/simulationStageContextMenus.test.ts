import { act, renderHook } from "@testing-library/react";
import { expect, describe, test } from "vitest";
import { EntityType } from "../models/enums/EntityType.ts";
import { PositionModel } from "../models/PositionModel.ts";
import { useSimulationStageContextMenusStore } from "./simulationStageContextMenus.ts";

describe("simulationStageContextMenusStore", () => {
    test("should set current context menu when setCurrentContextMenu is called", () => {
        const { result } = renderHook(() => useSimulationStageContextMenusStore());

        const position: PositionModel = { x: 100, y: 200 };
        const entityType = EntityType.VirtualMachine;
        const entityId = 42;

        act(() => {
            result.current.setCurrentContextMenu(entityType, entityId, position);
        });

        expect(result.current.currentEntityType).toEqual(entityType);
        expect(result.current.currentEntityId).toEqual(entityId);
        expect(result.current.currentPosition).toEqual(position);
    });

    test("should clear current context menu when clearCurrentContextMenu is called", () => {
        const { result } = renderHook(() => useSimulationStageContextMenusStore());

        act(() => {
            result.current.setCurrentContextMenu(EntityType.VirtualMachine, 99, { x: 10, y: 20 });
        });

        act(() => {
            result.current.clearCurrentContextMenu();
        });

        expect(result.current.currentEntityType).toBeNull();
        expect(result.current.currentEntityId).toBeNull();
    });
});
