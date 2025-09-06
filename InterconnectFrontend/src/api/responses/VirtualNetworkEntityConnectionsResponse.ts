import BaseResponse from "./BaseResponse.ts";
import VirtualNetworkEntityConnectionModel from "../../models/VirtualNetworkEntityConnectionModel.ts";

type VirtualNetworkEntityConnectionsResponse = BaseResponse<VirtualNetworkEntityConnectionModel[]>

export default VirtualNetworkEntityConnectionsResponse;