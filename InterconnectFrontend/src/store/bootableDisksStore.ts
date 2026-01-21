import { create } from "zustand";
import { BootableDiskModel } from "../models/BootableDiskModel.ts";
import { virtualMachineResourceClient } from "../api/resourceClient/VirtualMachineResourceClient.ts";

/**
 * State store for managing available bootable disks for virtual machine creation.
 */
interface BootableDisksStore {
    /** Array of available bootable disk options */
    bootableDisks: BootableDiskModel[];
    /** Loading state while fetching disks from backend */
    isFetching: boolean;
    /** Fetches available bootable disks from the VirtualMachine resource */
    fetchBootableDisks: () => Promise<void>;
}

/**
 * Zustand store hook for managing bootable disk inventory.
 * Fetches and caches available bootable disks from the backend.
 */
const useBootableDisksStore = create<BootableDisksStore>()((set) => ({
    bootableDisks: [],
    isFetching: true,
    fetchBootableDisks: async () => {
        set({
            isFetching: true,
        });
        const bootableDisks =
            await virtualMachineResourceClient.getAvailableBootableDisks();
        set({
            bootableDisks: bootableDisks.data,
            isFetching: false,
        });
    },
}));

export { useBootableDisksStore };
