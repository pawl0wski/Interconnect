import { VirtualMachineState } from "./enums/VirtualMachineState.ts";
import BaseEntity from "./interfaces/BaseEntity.ts";

interface VirtualMachineEntityModel extends BaseEntity {
    vmUuid: string | null;
    name: string;
    macAddress: string;
    state: VirtualMachineState;
}

export type { VirtualMachineEntityModel };
