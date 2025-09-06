import { beforeEach, expect, vi } from "vitest";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { act, renderHook } from "@testing-library/react";
import { useVirtualMachineEntitiesStore } from "./virtualMachineEntitiesStore.ts";

const mockUpdateEntityPosition = vi.hoisted(() => vi.fn());
const mockGetListOfEntities = vi.hoisted(() => vi.fn());

vi.mock("../api/resourceClient/VirtualMachineEntityResourceClient.ts", () => ({
    virtualMachineEntityResourceClient: {
        updateEntityPosition: mockUpdateEntityPosition,
        getListOfEntities: mockGetListOfEntities
    }
}));

describe("virtualMachineEntitiesStore", () => {
    beforeEach(() => {
        mockUpdateEntityPosition.mockReset();
        mockGetListOfEntities.mockReset();

        const { result } = renderHook(() => useVirtualMachineEntitiesStore());
        act(() => {
            result.current.clearEntities();
        });
    });

    test("should fetch entities from api when fetchEntities is invoked", async () => {
        mockGetListOfEntities.mockReturnValueOnce(
            {
                data: [
                    {
                        id: 1,
                        vmUuid: null,
                        name: "Test1",
                        x: 12,
                        y: 54
                    },
                    {
                        id: 2,
                        vmUuid: null,
                        name: "Test2",
                        x: 43,
                        y: 52
                    }
                ] as VirtualMachineEntityModel[]
            }
        );
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.fetchEntities();
        });

        expect(mockGetListOfEntities).toHaveBeenCalled();
        expect(result.current.entities).toHaveLength(2);
        expect(result.current.entities[0].name).toBe("Test1");
        expect(result.current.entities[1].name).toBe("Test2");
    });

    test("should update entity position when updateEntityPosition is invoked with final update", async () => {
        mockGetListOfEntities.mockReturnValueOnce(
            {
                data: [
                    {
                        id: 1,
                        vmUuid: null,
                        name: "Test1",
                        x: 12,
                        y: 54
                    }
                ] as VirtualMachineEntityModel[]
            }
        );
        mockUpdateEntityPosition.mockReturnValueOnce({
            data: {
                id: 1,
                vmUuid: null,
                name: "Test1",
                x: 54,
                y: 33
            }
        });
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.fetchEntities();
            await result.current.updateEntityPosition(1, 54, 33, true);
        });
        const { name, id, x, y } = result.current.entities[0];

        expect(mockUpdateEntityPosition).toHaveBeenCalledWith(1, 54, 33);
        expect(name).toBe("Test1");
        expect(id).toBe(1);
        expect(x).toBe(54);
        expect(y).toBe(33);
    });

    test("should not update entity position when updateEntityPosition is invoked without final update", async () => {
        mockGetListOfEntities.mockReturnValueOnce(
            {
                data: [
                    {
                        id: 1,
                        vmUuid: null,
                        name: "Test1",
                        x: 12,
                        y: 54
                    }
                ] as VirtualMachineEntityModel[]
            }
        );
        mockUpdateEntityPosition.mockReturnValueOnce({
            data: {
                id: 1,
                vmUuid: null,
                name: "Test1",
                x: 54,
                y: 33
            }
        });
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.fetchEntities();
            await result.current.updateEntityPosition(1, 54, 33, false);
        });

        expect(mockUpdateEntityPosition).not.toHaveBeenCalledWith(1, 54, 33);
    });

    test("should not modify anything if the user changes the position of a non-existent entity", async () => {
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.updateEntityPosition(1, 12, 12);
        });

        expect(mockUpdateEntityPosition).not.toHaveBeenCalled();
        expect(result.current.entities).toHaveLength(0);
    });

    test("should get entity by id", async () => {
        mockGetListOfEntities.mockReturnValueOnce(
            {
                data: [
                    {
                        id: 1,
                        vmUuid: null,
                        name: "Test1",
                        x: 12,
                        y: 54
                    },
                    {
                        id: 2,
                        vmUuid: null,
                        name: "Test2",
                        x: 43,
                        y: 52
                    }
                ] as VirtualMachineEntityModel[]
            }
        );
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());
        let foundEntity: VirtualMachineEntityModel | undefined;

        await act(async () => {
            await result.current.fetchEntities();
            foundEntity = result.current.getById(2);
        });

        expect(foundEntity?.name).toBe("Test2");
    });
});