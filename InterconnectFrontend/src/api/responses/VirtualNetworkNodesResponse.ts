import BaseResponse from "./BaseResponse.ts";
import VirtualNetworkNodeEntityModel from "../../models/VirtualNetworkNodeEntityModel.ts";

type VirtualNetworkNodesResponse = BaseResponse<VirtualNetworkNodeEntityModel[]>;

export default VirtualNetworkNodesResponse;
