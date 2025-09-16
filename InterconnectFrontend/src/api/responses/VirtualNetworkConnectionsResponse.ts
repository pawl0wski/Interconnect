import BaseResponse from "./BaseResponse.ts";
import VirtualNetworkConnectionModel from "../../models/VirtualNetworkConnectionModel.ts";

type VirtualNetworkConnectionsResponse = BaseResponse<
    VirtualNetworkConnectionModel[]
>;

export default VirtualNetworkConnectionsResponse;
