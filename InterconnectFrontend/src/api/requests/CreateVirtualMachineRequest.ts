import BaseRequest from "./BaseRequest.ts";

interface CreateVirtualMachineRequest extends BaseRequest {
    name: string;
    memory: number;
    virtualCpus: number;
    bootableDiskId: number;
}

export default CreateVirtualMachineRequest;