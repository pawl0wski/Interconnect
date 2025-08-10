import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";

class VirtualMachineManagerResourceClient extends BaseBackendResourceClient {
    protected getResourceName(): string {
        return "VirtualMachineManager";
    }
}

const virtualMachineManagerResourceClient = new VirtualMachineManagerResourceClient();

export { virtualMachineManagerResourceClient };