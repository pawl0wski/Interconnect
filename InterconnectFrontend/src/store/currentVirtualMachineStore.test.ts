import { act, renderHook } from "@testing-library/react";
import { useCurrentVirtualMachineStore } from "./currentVirtualMachineStore.ts";
import { expect } from "vitest";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

describe("currentVirtualMachineStore", () => {
    test("should update entity when updateEntity is called", async () => {
        const entityToUpdate = { vmUuid: "123", name: "TestVm" } as VirtualMachineEntityModel;
        const { result } = renderHook(() => useCurrentVirtualMachineStore());

        act(() => {
            result.current.setCurrentEntity(entityToUpdate);
        });

        expect(result.current.currentEntity).equal(entityToUpdate);
    });
});