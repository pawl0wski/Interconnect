import { describe, expect, test, vi, beforeEach } from "vitest";
import { renderHook, act } from "@testing-library/react";
import { useConnectionStore } from "./connectionStore.ts";

const mockConnectionStatus = vi.hoisted(() => vi.fn());

vi.mock("../api/HypervisorConnectionResourceClient.ts", () => ({
    hypervisorConnectionClient: {
        connectionStatus: mockConnectionStatus
    }
}));

describe("connectionStore", () => {
    beforeEach(() => {
        mockConnectionStatus.mockClear();
    });

    test("should update connection status when updateConnectionStatus is called", async () => {
        mockConnectionStatus.mockResolvedValue({
            success: true,
            message: "",
            data: 1
        });

        const { result } = renderHook(() => useConnectionStore());

        await act(async () => {
            await result.current.updateConnectionStatus();
        });

        expect(mockConnectionStatus).toHaveBeenCalled();
    });
});