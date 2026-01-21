import BaseRequest from "./BaseRequest.ts";
import VirtualMachineEntityType from "../../models/enums/VirtualMachineEntityType.ts";

/**
 * Request object for creating a new virtual machine.
 */
interface CreateVirtualMachineRequest extends BaseRequest {
    /** Display name for the virtual machine */
    name: string;
    /** Memory allocation in MB */
    memory: number;
    /** Number of virtual CPUs to allocate */
    virtualCpus: number;
    /** Type/role of the virtual machine */
    type: VirtualMachineEntityType;
    /** ID of the bootable disk to use */
    bootableDiskId: number;
}

export default CreateVirtualMachineRequest;
