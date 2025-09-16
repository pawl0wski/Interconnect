import { VirtualMachineEntityModel } from "../../models/VirtualMachineEntityModel.ts";
import BaseResponse from "./BaseResponse.ts";

type VirtualMachinesEntitiesResponse = BaseResponse<
    VirtualMachineEntityModel[]
>;

export default VirtualMachinesEntitiesResponse;
