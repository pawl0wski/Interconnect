import { expect, vi } from "vitest";
import { act, renderHook } from "@testing-library/react";
import { useBootableDisksStore } from "./bootableDisksStore.ts";

const mockGetAvailableBootableDisks = vi.hoisted(() => vi.fn());

vi.mock("../api/resourceClient/VirtualMachineResourceClient.ts", () => ({
    virtualMachineResourceClient: {
        getAvailableBootableDisks: mockGetAvailableBootableDisks
    }
}));

describe("bootableDiskStore", () => {
    test("should fetch bootable disks from api", async () => {
        mockGetAvailableBootableDisks.mockResolvedValue({
            "data": [
                {
                    "id": 128,
                    "vmUuid": "fed610a3-e346-415c-b966-73c86d4b1cf8",
                    "name": "Test",
                    "x": 944,
                    "y": 379
                }
            ]
        });
        const { result } = renderHook(() => useBootableDisksStore());

        await act(async () => {
            await result.current.fetchBootableDisks();
        });

        expect(result.current.bootableDisks.length).toBe(1);
        expect(result.current.bootableDisks[0]).toStrictEqual({
            "id": 128,
            "vmUuid": "fed610a3-e346-415c-b966-73c86d4b1cf8",
            "name": "Test",
            "x": 944,
            "y": 379
        });
    });
});