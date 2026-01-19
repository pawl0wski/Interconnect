import BaseResponse from "./BaseResponse.ts";
import VirtualMachineEntitiesWithMacAddresses from "../../models/VirtualMachineEntitiesWithMacAddresses.ts";

type VirtualMachinesEntitiesResponse = BaseResponse<
    VirtualMachineEntitiesWithMacAddresses
>;

export default VirtualMachinesEntitiesResponse;
