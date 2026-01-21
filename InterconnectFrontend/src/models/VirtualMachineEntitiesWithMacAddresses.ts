import { VirtualMachineEntityModel } from "./VirtualMachineEntityModel.ts";
import VirtualMachineEntityMacAddress from "./VirtualMachineEntityMacAddress.ts";

/**
 * Compound model containing both virtual machine entities and their associated MAC addresses.
 */
interface VirtualMachineEntitiesWithMacAddresses {
    /** Array of virtual machine entities */
    virtualMachineEntities: VirtualMachineEntityModel[];
    /** Array of MAC address mappings for the virtual machines */
    macAddressEntities: VirtualMachineEntityMacAddress[];
}

export default VirtualMachineEntitiesWithMacAddresses;