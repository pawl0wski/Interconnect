import { create } from "zustand/react";
import { virtualMachineResourceClient } from "../api/resourceClient/VirtualMachineResourceClient.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { virtualMachineEntityResourceClient } from "../api/resourceClient/VirtualMachineEntityResourceClient.ts";

interface VirtualMachineCreateStore {
    name: string,
    memory: number,
    virtualCPUs: number,
    bootableDiskId: number,
    x: number,
    y: number,
    update: (partial: Partial<Omit<VirtualMachineCreateStore, "update">>) => void;
    createVirtualMachine: () => Promise<VirtualMachineEntityModel>;
}

const useVirtualMachineCreateStore = create<VirtualMachineCreateStore>()((set, get) => ({
    name: "",
    memory: 0,
    virtualCPUs: 0,
    bootableDiskId: 0,
    x: 0,
    y: 0,
    update: (partial) => set(partial),
    createVirtualMachine: async () => {
        const { name, memory, virtualCPUs, bootableDiskId, x, y } = get();

        const response = await virtualMachineResourceClient.createVirtualMachine({
            name: name,
            memory: memory,
            virtualCpus: virtualCPUs,
            bootableDiskId: bootableDiskId
        });
        await virtualMachineEntityResourceClient.updateEntityPosition(response.data.id, x, y);
        return response.data;
    }
}));

export { useVirtualMachineCreateStore };

