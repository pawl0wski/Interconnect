import { create } from "zustand/react";
import { virtualMachineResourceClient } from "../api/resourceClient/VirtualMachineResourceClient.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

interface VirtualMachineCreateStore {
    name: string,
    memory: number,
    virtualCPUs: number,
    bootableDiskId: number,
    update: (partial: Partial<Omit<VirtualMachineCreateStore, "update">>) => void;
    createVirtualMachine: () => Promise<VirtualMachineEntityModel>;
}

const useVirtualMachineCreateStore = create<VirtualMachineCreateStore>()((set, get) => ({
    name: "",
    memory: 0,
    virtualCPUs: 0,
    bootableDiskId: 0,
    update: (partial) => set(partial),
    createVirtualMachine: async () => {
        const response = await virtualMachineResourceClient.createVirtualMachine({
            name: get().name,
            memory: get().memory,
            virtualCpus: get().virtualCPUs,
            bootableDiskId: get().bootableDiskId
        });
        return response.data;
    }
}));

export { useVirtualMachineCreateStore };

