import { VirtualMachineEntityModel } from "./VirtualMachineEntityModel.ts";
import VirtualMachineEntityMacAddress from "./VirtualMachineEntityMacAddress.ts";

interface VirtualMachineEntitiesWithMacAddresses {
    virtualMachineEntities: VirtualMachineEntityModel[];
    macAddressEntities: VirtualMachineEntityMacAddress[];
}

export default VirtualMachineEntitiesWithMacAddresses;