import { VirtualMachineState } from "./enums/VirtualMachineState.ts";

interface VirtualMachineEntityModel {
    id: number;
    vmUuid: string | null;
    name: string;
    state: VirtualMachineState;
    x: number;
    y: number;
}

export type { VirtualMachineEntityModel };