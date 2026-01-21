import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

/**
 * State store for tracking the currently selected virtual machine entity.
 */
interface CurrentVirtualMachineStore {
    /** The currently selected virtual machine entity, or null if none selected */
    currentEntity: VirtualMachineEntityModel | null;
    /** Sets the currently selected virtual machine entity */
    setCurrentEntity: (entity: VirtualMachineEntityModel) => void;
}

/**
 * Zustand store hook for managing the active virtual machine selection.
 * Used to track which VM is currently being viewed or interacted with.
 */
const useCurrentVirtualMachineStore = create<CurrentVirtualMachineStore>(
    (set) => ({
        currentEntity: null,
        setCurrentEntity: (entity: VirtualMachineEntityModel) =>
            set({ currentEntity: entity }),
    }),
);

export { useCurrentVirtualMachineStore };
