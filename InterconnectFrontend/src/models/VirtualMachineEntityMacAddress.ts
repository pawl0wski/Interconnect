/**
 * Represents MAC addresses associated with a virtual machine entity.
 */
interface VirtualMachineEntityMacAddress {
    /** ID of the virtual machine entity */
    virtualMachineEntityId: number;
    /** Array of MAC addresses assigned to this virtual machine */
    macAddresses: string[];
}

export default VirtualMachineEntityMacAddress;