import { create } from "zustand/react";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import entityResourceClient from "../api/resourceClient/EntityResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";
import VirtualMachineEntityType from "../models/enums/VirtualMachineEntityType.ts";

/**
 * State store for managing virtual machine creation form data and submission.
 */
interface VirtualMachineCreateStore {
    /** Virtual machine name/display name */
    name: string;
    /** Type of virtual machine (Host, Router, Server) */
    type: VirtualMachineEntityType;
    /** Memory allocation in MB */
    memory: number;
    /** Number of virtual CPU cores */
    virtualCPUs: number;
    /** ID of the bootable disk to use */
    bootableDiskId: number;
    /** X position on simulation stage canvas */
    x: number;
    /** Y position on simulation stage canvas */
    y: number;
    /** Updates one or more form fields */
    update: (
        partial: Partial<Omit<VirtualMachineCreateStore, "update" | "createVirtualMachine">>,
    ) => void;
    /** Creates the virtual machine on backend and returns the created entity */
    createVirtualMachine: () => Promise<VirtualMachineEntityModel>;
}

/**
 * Zustand store hook for managing virtual machine creation workflow.
 * Tracks form state and handles VM creation API calls.
 */
const useVirtualMachineCreateStore = create<VirtualMachineCreateStore>()(
    (set, get) => ({
        name: "",
        type: VirtualMachineEntityType.Host,
        memory: 0,
        virtualCPUs: 0,
        bootableDiskId: 0,
        x: 0,
        y: 0,
        update: (partial) => set(partial),
        createVirtualMachine: async () => {
            const { name, type, memory, virtualCPUs, bootableDiskId, x, y } =
                get();

            const response =
                await entityResourceClient.createVirtualMachineEntity({
                    name: name,
                    type: type,
                    memory: memory,
                    virtualCpus: virtualCPUs,
                    bootableDiskId: bootableDiskId,
                });
            await entityResourceClient.updateEntityPosition({
                id: response.data.id,
                type: EntityType.VirtualMachine,
                x,
                y,
            });
            return response.data;
        },
    }),
);

export { useVirtualMachineCreateStore };
