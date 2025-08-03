import { create } from "zustand";
import { BootableDiskModel } from "../models/BootableDiskModel.ts";
import { virtualMachineManagerResourceClient } from "../api/VirtualMachineManagerResourceClient.ts";

interface BootableDisksStore {
    bootableDisks: BootableDiskModel[];
    isFetching: boolean;
    fetchBootableDisks: () => Promise<void>;
}

const useBootableDisksStore = create<BootableDisksStore>()((set) => ({
    bootableDisks: [],
    isFetching: true,
    fetchBootableDisks: async () => {
        set({
            isFetching: true
        });
        const bootableDisks = await virtualMachineManagerResourceClient.getAvailableBootableDisks();
        set({
            bootableDisks: bootableDisks.data,
            isFetching: false
        });
    }
}));

export { useBootableDisksStore };