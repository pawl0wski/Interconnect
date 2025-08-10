import { create } from "zustand/react";
import { virtualMachineResourceClient } from "../api/resourceClient/VirtualMachineResourceClient.ts";

interface VirtualMachineCreateStore {
    name: string,
    memory: number,
    virtualCPUs: number,
    bootableDiskId: number,
    update: (partial: Partial<Omit<VirtualMachineCreateStore, "update">>) => void;
    createVirtualMachine: () => Promise<void>;
}

const useVirtualMachineCreateStore = create<VirtualMachineCreateStore>()((set, get) => ({
    name: "",
    memory: 0,
    virtualCPUs: 0,
    bootableDiskId: 0,
    update: (partial) => set(partial),
    createVirtualMachine: async () => {
        await virtualMachineResourceClient.createVirtualMachine({
            name: get().name,
            memory: get().memory,
            virtualCpus: get().virtualCPUs,
            bootableDiskId: get().bootableDiskId
        });
    }
}));

export { useVirtualMachineCreateStore };

