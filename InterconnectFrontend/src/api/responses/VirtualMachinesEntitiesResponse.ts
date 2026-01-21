import BaseResponse from "./BaseResponse.ts";
import VirtualMachineEntitiesWithMacAddresses from "../../models/VirtualMachineEntitiesWithMacAddresses.ts";

/**
 * Response type for virtual machine entities with their associated MAC addresses.
 */
type VirtualMachinesEntitiesResponse = BaseResponse<
    VirtualMachineEntitiesWithMacAddresses
>;

export default VirtualMachinesEntitiesResponse;
