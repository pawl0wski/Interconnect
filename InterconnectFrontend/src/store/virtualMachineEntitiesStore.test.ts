import { beforeEach, vi } from "vitest";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { act, renderHook } from "@testing-library/react";
import { useVirtualMachineEntitiesStore } from "./virtualMachineEntitiesStore.ts";

const mockCreateEntity = vi.hoisted(() => vi.fn());
const mockUpdateEntityPosition = vi.hoisted(() => vi.fn());
const mockGetListOfEntities = vi.hoisted(() => vi.fn());

vi.mock("../api/VirtualMachineEntityResourceClient.ts", () => ({
    virtualMachineEntityResourceClient: {
        createEntity: mockCreateEntity,
        updateEntityPosition: mockUpdateEntityPosition,
        getListOfEntities: mockGetListOfEntities
    }
}));

describe("virtualMachineEntitiesStore", () => {
    beforeEach(() => {
        mockCreateEntity.mockReset();
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

    test("should create new entity when createNewEntity is invoked", async () => {
        mockCreateEntity.mockReturnValueOnce({
            data: {
                id: 1,
                vmUuid: null,
                name: "Test",
                x: 12,
                y: 54
            }
        });
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.createNewEntity("Test", 12, 54);
        });
        const { name, x, y } = result.current.entities[0];

        expect(mockCreateEntity).toHaveBeenCalled();
        expect(name).toBe("Test");
        expect(x).toBe(12);
        expect(y).toBe(54);
    });

    test("should update entity position when updateEntityPosition is invoked", async () => {
        mockCreateEntity.mockReturnValueOnce({
            data: {
                id: 1,
                vmUuid: null,
                name: "Test",
                x: 12,
                y: 54
            }
        });
        mockUpdateEntityPosition.mockReturnValueOnce({
            data: {
                id: 1,
                vmUuid: null,
                name: "Test",
                x: 54,
                y: 33
            }
        });
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.createNewEntity("Test", 12, 54);
            await result.current.updateEntityPosition(1, 54, 33);
        });
        const { name, id, x, y } = result.current.entities[0];

        expect(mockUpdateEntityPosition).toHaveBeenCalledWith(1, 54, 33);
        expect(name).toBe("Test");
        expect(id).toBe(1);
        expect(x).toBe(54);
        expect(y).toBe(33);
    });

    test("should not modify anything if the user changes the position of a non-existent entity", async () => {
        const { result } = renderHook(() => useVirtualMachineEntitiesStore());

        await act(async () => {
            await result.current.updateEntityPosition(1, 12, 12);
        });

        expect(mockUpdateEntityPosition).not.toHaveBeenCalled();
        expect(result.current.entities).toHaveLength(0);
    });

});