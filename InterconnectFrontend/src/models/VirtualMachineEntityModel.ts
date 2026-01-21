import { VirtualMachineState } from "./enums/VirtualMachineState.ts";
import BaseEntity from "./interfaces/BaseEntity.ts";
import VirtualMachineEntityType from "./enums/VirtualMachineEntityType.ts";

/**
 * Represents a virtual machine entity in the simulation.
 */
interface VirtualMachineEntityModel extends BaseEntity {
    /** UUID of the virtual machine or null if not yet created */
    vmUuid: string | null;
    /** Display name of the virtual machine */
    name: string;
    /** Array of MAC addresses assigned to this virtual machine */
    macAddresses: string[];
    /** Type/role of the virtual machine in the simulation */
    type: VirtualMachineEntityType;
    /** Current state of the virtual machine (running, stopped, etc.) */
    state: VirtualMachineState;
}

export type { VirtualMachineEntityModel };
