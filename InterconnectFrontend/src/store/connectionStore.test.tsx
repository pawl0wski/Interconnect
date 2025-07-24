import { describe, expect, test, vi, beforeEach } from "vitest";
import { renderHook, act } from "@testing-library/react";
import { useConnectionStore } from "./connectionStore.ts";

const mockPing = vi.hoisted(() => vi.fn());
vi.mock("../api/HypervisorConnectionResourceClient.ts", () => ({
    hypervisorConnectionClient: {
        ping: mockPing
    }
}));

describe("connectionStore", () => {
    beforeEach(() => {
        mockPing.mockClear();
    });

    test("should update connection status when updateConnectionStatus is called", async () => {
        mockPing.mockResolvedValue({
            success: true,
            message: "pong",
            data: 1
        });
        const { result } = renderHook(() => useConnectionStore());

        await act(async () => {
            await result.current.updateConnectionStatus();
        });

        expect(mockPing).toHaveBeenCalled();
    });
});