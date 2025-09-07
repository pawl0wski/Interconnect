import BaseResponse from "./BaseResponse.ts";
import VirtualSwitchEntityModel from "../../models/VirtualSwitchEntityModel.ts";

type VirtualSwitchesResponse = BaseResponse<VirtualSwitchEntityModel[]>;

export default VirtualSwitchesResponse;