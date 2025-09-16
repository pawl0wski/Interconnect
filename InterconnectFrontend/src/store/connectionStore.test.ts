import { beforeEach, describe, expect, test, vi } from "vitest";
import { act, renderHook } from "@testing-library/react";
import { useConnectionStore } from "./connectionStore.ts";

const mockPing = vi.hoisted(() => vi.fn());
vi.mock("../api/hubClient/ConnectionStatusHubClient.ts", () => ({
    connectionStatusHubClient: {
        ping: mockPing,
    },
}));

describe("connectionStore", () => {
    beforeEach(() => {
        mockPing.mockClear();
    });

    test("should update connection status when updateConnectionStatus is called", async () => {
        mockPing.mockResolvedValue({
            success: true,
            message: "pong",
            data: 1,
        });
        const { result } = renderHook(() => useConnectionStore());

        await act(async () => {
            await result.current.updateConnectionStatus();
        });

        expect(mockPing).toHaveBeenCalled();
    });
});
