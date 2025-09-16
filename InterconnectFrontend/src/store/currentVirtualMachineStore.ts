import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

interface CurrentVirtualMachineStore {
    currentEntity: VirtualMachineEntityModel | null;
    setCurrentEntity: (entity: VirtualMachineEntityModel) => void;
}

const useCurrentVirtualMachineStore = create<CurrentVirtualMachineStore>(
    (set) => ({
        currentEntity: null,
        setCurrentEntity: (entity: VirtualMachineEntityModel) =>
            set({ currentEntity: entity }),
    }),
);

export { useCurrentVirtualMachineStore };
