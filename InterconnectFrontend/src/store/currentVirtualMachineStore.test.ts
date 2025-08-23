import { act, renderHook } from "@testing-library/react";
import { useCurrentVirtualMachineStore } from "./currentVirtualMachineStore.ts";
import { expect } from "vitest";

describe("currentVirtualMachineStore", () => {
    test("should update uuid when updateUuid is called", async () => {
        const { result } = renderHook(() => useCurrentVirtualMachineStore());

        act(() => {
            result.current.setUuid("123");
        });

        expect(result.current.uuid).equal("123");
    });
});