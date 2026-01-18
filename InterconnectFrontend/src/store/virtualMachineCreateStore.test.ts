import { beforeEach, vi } from "vitest";
import { act, renderHook } from "@testing-library/react";
import { useVirtualMachineCreateStore } from "./virtualMachineCreateStore.ts";
import VirtualMachineEntityType from "../models/enums/VirtualMachineEntityType.ts";

const mockCreateVirtualMachine = vi.hoisted(() => vi.fn());
const mockUpdateEntityPosition = vi.hoisted(() => vi.fn());

vi.mock("../api/resourceClient/EntityResourceClient.ts", () => ({
    default: {
        createVirtualMachineEntity: mockCreateVirtualMachine,
        updateEntityPosition: mockUpdateEntityPosition,
    },
}));

describe("virtualMachineCreateStore", () => {
    beforeEach(() => {
        mockCreateVirtualMachine.mockReset();
    });

    test("should update virtual machine definition when update is invoked", () => {
        const { result } = renderHook(() => useVirtualMachineCreateStore());

        act(() => {
            result.current.update({
                name: "Test",
                virtualCPUs: 4,
                memory: 1024,
                bootableDiskId: 1,
            });
        });

        expect(result.current.name).toBe("Test");
        expect(result.current.virtualCPUs).toBe(4);
        expect(result.current.memory).toBe(1024);
        expect(result.current.bootableDiskId).toBe(1);
    });

    test("should create virtual machine when createVirtualMachine is invoked", async () => {
        mockCreateVirtualMachine.mockReturnValueOnce({
            data: [
                {
                    id: 1,
                    vmUuid: "d57ba30f-118d-4d22-8c82-a1cca2dc23a6",
                    name: "Test",
                    x: 25,
                    y: 25,
                },
            ],
        });
        const { result } = renderHook(() => useVirtualMachineCreateStore());

        await act(async () => {
            result.current.update({
                name: "Test",
                virtualCPUs: 4,
                memory: 1024,
                bootableDiskId: 1,
                x: 43,
                y: 25,
            });
            await result.current.createVirtualMachine();
        });

        expect(mockCreateVirtualMachine).toHaveBeenCalledWith({
            name: "Test",
            type: VirtualMachineEntityType.Host,
            memory: 1024,
            virtualCpus: 4,
            bootableDiskId: 1,
        });
        expect(mockUpdateEntityPosition).toHaveBeenCalledWith({
            id: 1,
            type: 1,
            x: 43,
            y: 25,
        });
    });
});
