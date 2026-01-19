import { VirtualMachineState } from "./enums/VirtualMachineState.ts";
import BaseEntity from "./interfaces/BaseEntity.ts";
import VirtualMachineEntityType from "./enums/VirtualMachineEntityType.ts";

interface VirtualMachineEntityModel extends BaseEntity {
    vmUuid: string | null;
    name: string;
    macAddresses: string[];
    type: VirtualMachineEntityType;
    state: VirtualMachineState;
}

export type { VirtualMachineEntityModel };
