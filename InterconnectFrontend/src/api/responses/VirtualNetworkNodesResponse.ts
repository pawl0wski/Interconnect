import BaseResponse from "./BaseResponse.ts";
import VirtualNetworkNodeEntityModel from "../../models/VirtualNetworkNodeEntityModel.ts";

/**
 * Response type for a list of virtual network node entities.
 */
type VirtualNetworkNodesResponse = BaseResponse<VirtualNetworkNodeEntityModel[]>;

export default VirtualNetworkNodesResponse;
