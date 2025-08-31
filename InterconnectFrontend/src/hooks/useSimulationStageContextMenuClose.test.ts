import { renderHook, act } from "@testing-library/react";
import { describe, test, expect, vi, beforeEach } from "vitest";
import useSimulationStageContextMenuClose from "./useSimulationStageContextMenuClose";

const { mockClear } = vi.hoisted(() => ({
    mockClear: vi.fn()
}));

vi.mock("../store/simulationStageContextMenus.ts", () => ({
    useSimulationStageContextMenusStore: () => ({
        clearCurrentContextMenu: mockClear
    })
}));

describe("useSimulationStageContextMenuClose", () => {
    beforeEach(() => {
        mockClear.mockClear();
    });

    test("should call clearCurrentContextMenu when closeContextMenu is invoked", () => {
        const { result } = renderHook(() => useSimulationStageContextMenuClose());

        act(() => {
            result.current.closeContextMenu();
        });

        expect(mockClear).toHaveBeenCalled();
    });
});
