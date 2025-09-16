import { renderHook } from "@testing-library/react";
import { beforeEach, describe, expect, test, vi } from "vitest";
import { useSimulationStageContextMenuInfo } from "./useSimulationStageContextMenuInfo";
import { PositionModel } from "../models/PositionModel";
import { EntityType } from "../models/enums/EntityType";

const { mockStore } = vi.hoisted(() => ({
    mockStore: {
        currentEntityType: null as EntityType | null,
        currentEntityId: null,
        currentPosition: { x: 0, y: 0 },
    },
}));

vi.mock("../store/simulationStageContextMenus.ts", () => ({
    useSimulationStageContextMenusStore: () => mockStore,
}));

describe("useSimulationStageContextMenuInfo", () => {
    beforeEach(() => {
        mockStore.currentEntityType = null;
        mockStore.currentEntityId = null;
        mockStore.currentPosition = { x: 0, y: 0 };
    });

    test("should return visible=true and correct position when entityType matches", () => {
        const position: PositionModel = { x: 123, y: 456 };
        mockStore.currentEntityType = EntityType.VirtualMachine;
        mockStore.currentPosition = position;

        const { result } = renderHook(() =>
            useSimulationStageContextMenuInfo(EntityType.VirtualMachine),
        );

        expect(result.current.visible).toBe(true);
        expect(result.current.position).toEqual(position);
    });

    test("should return visible=false when entityType does not match", () => {
        mockStore.currentEntityType = EntityType.Network;
        mockStore.currentPosition = { x: 111, y: 222 };

        const { result } = renderHook(() =>
            useSimulationStageContextMenuInfo(EntityType.VirtualMachine),
        );

        expect(result.current.visible).toBe(false);
        expect(result.current.position).toEqual({ x: 0, y: 0 });
    });
});
