import { beforeEach, describe, expect, test, vi } from "vitest";
import { act, renderHook } from "@testing-library/react";
import { useConnectionInfoStore } from "./connectionInfoStore.ts";

const mockConnectionInfo = vi.hoisted(() => vi.fn());
vi.mock("../api/resourceClient/HypervisorConnectionResourceClient.ts", () => ({
    hypervisorConnectionClient: {
        connectionInfo: mockConnectionInfo,
    },
}));

describe("connectionStore", () => {
    beforeEach(() => {
        mockConnectionInfo.mockClear();
    });

    test("should fetch connection info when updateConnectionInfo is invoked", async () => {
        mockConnectionInfo.mockResolvedValue({
            success: true,
            message: { testKey: "testValue" },
            data: 1,
        });
        const { result } = renderHook(() => useConnectionInfoStore());

        await act(async () => {
            await result.current.updateConnectionInfo();
        });

        expect(mockConnectionInfo).toHaveBeenCalled();
    });
});
