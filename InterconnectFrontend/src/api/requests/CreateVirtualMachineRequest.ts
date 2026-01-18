import BaseRequest from "./BaseRequest.ts";
import VirtualMachineEntityType from "../../models/enums/VirtualMachineEntityType.ts";

interface CreateVirtualMachineRequest extends BaseRequest {
    name: string;
    memory: number;
    virtualCpus: number;
    type: VirtualMachineEntityType;
    bootableDiskId: number;
}

export default CreateVirtualMachineRequest;
