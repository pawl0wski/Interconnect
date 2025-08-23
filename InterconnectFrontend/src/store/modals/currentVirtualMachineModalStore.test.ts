import { describe, expect, test } from "vitest";
import { act, renderHook } from "@testing-library/react";
import { useCurrentVirtualMachineModalStore } from "./currentVirtualMachineModalStore.ts";

describe("currentVirtualMachineModalStore", () => {
    test("should open modal when open is invoked", () => {
        const { result } = renderHook(() => useCurrentVirtualMachineModalStore());

        act(() => {
            result.current.close();
            result.current.open();
        });

        expect(result.current.opened).toBeTruthy();
    });
    test("should close modal when close is invoked", () => {
        const { result } = renderHook(() => useCurrentVirtualMachineModalStore());

        act(() => {
            result.current.open();
            result.current.close();
        });

        expect(result.current.opened).toBeFalsy();
    });
});