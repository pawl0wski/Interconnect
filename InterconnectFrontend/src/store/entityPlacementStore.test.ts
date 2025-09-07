import { act, renderHook } from "@testing-library/react";
import { expect, describe, test, vi, beforeEach } from "vitest";
import { useEntityPlacementStore } from "./entityPlacementStore.ts";
import { EntityType } from "../models/enums/EntityType.ts";

const { mockUpdate, mockOpen } = vi.hoisted(() => ({
    mockUpdate: vi.fn(),
    mockOpen: vi.fn()
}));

vi.mock("./virtualMachineCreateStore.ts", () => ({
    useVirtualMachineCreateStore: {
        getState: vi.fn(() => ({ update: mockUpdate }))
    }
}));

vi.mock("./modals/modalStores.ts", async () => ({
    ...(await vi.importActual("./modals/modalStores.ts")),
    useVirtualMachineCreateModalStore: {
        getState: vi.fn(() => ({ open: mockOpen }))
    }
}));

describe("entityPlacementStore", () => {
    beforeEach(() => {
        mockUpdate.mockClear();
        mockOpen.mockClear();
    });

    test("should set current entity when setCurrentEntity is called", () => {
        const { result } = renderHook(() => useEntityPlacementStore());

        act(() => {
            result.current.setCurrentEntityType(EntityType.VirtualMachine);
        });

        expect(result.current.currentEntityType).toEqual(EntityType.VirtualMachine);
    });

    test("should call update and open modal when placing VirtualMachine", () => {
        const { result } = renderHook(() => useEntityPlacementStore());

        act(() => {
            result.current.setCurrentEntityType(EntityType.VirtualMachine);
        });

        act(() => {
            result.current.placeCurrentEntity(50, 75);
        });

        expect(mockUpdate).toHaveBeenCalledWith({ x: 50, y: 75 });
        expect(mockOpen).toHaveBeenCalled();
        expect(result.current.currentEntityType).toBeNull();
    });

    test("should reset entity when discardPlacingEntity is called", () => {
        const { result } = renderHook(() => useEntityPlacementStore());

        act(() => {
            result.current.setCurrentEntityType(EntityType.VirtualMachine);
        });

        act(() => {
            result.current.discardPlacingEntity();
        });

        expect(result.current.currentEntityType).toBeNull();
    });
});
