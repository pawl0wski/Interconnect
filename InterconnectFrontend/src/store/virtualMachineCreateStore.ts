import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";

interface VirtualMachineCreateStore {
    name: string;
    memory: number;
    virtualCPUs: number;
    bootableDiskId: number;
    x: number;
    y: number;
    update: (
        partial: Partial<Omit<VirtualMachineCreateStore, "update">>,
    ) => void;
    createVirtualMachine: () => Promise<VirtualMachineEntityModel>;
}

const useVirtualMachineCreateStore = create<VirtualMachineCreateStore>()(
    (set, get) => ({
        name: "",
        memory: 0,
        virtualCPUs: 0,
        bootableDiskId: 0,
        x: 0,
        y: 0,
        update: (partial) => set(partial),
        createVirtualMachine: async () => {
            const { name, memory, virtualCPUs, bootableDiskId, x, y } = get();

            const response =
                await entityResourceClient.createVirtualMachineEntity({
                    name: name,
                    memory: memory,
                    virtualCpus: virtualCPUs,
                    bootableDiskId: bootableDiskId,
                });
            await entityResourceClient.updateEntityPosition({
                id: response.data[0].id,
                type: EntityType.VirtualMachine,
                x,
                y,
            });
            return response.data[0];
        },
    }),
);

export { useVirtualMachineCreateStore };
