import BaseResponse from "./BaseResponse.ts";
import VirtualNetworkConnectionModel from "../../models/VirtualNetworkConnectionModel.ts";

/**
 * Response type for a list of virtual network connections.
 */
type VirtualNetworkConnectionsResponse = BaseResponse<
    VirtualNetworkConnectionModel[]
>;

export default VirtualNetworkConnectionsResponse;
