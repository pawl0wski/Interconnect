import BaseResponse from "./BaseResponse.ts";
import { VirtualMachineEntityModel } from "../../models/VirtualMachineEntityModel.ts";

/**
 * Response type for a single virtual machine entity.
 */
type VirtualMachineEntityResponse = BaseResponse<VirtualMachineEntityModel>;

export default VirtualMachineEntityResponse;
