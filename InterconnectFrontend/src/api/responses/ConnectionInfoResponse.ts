import BaseResponse from "./BaseResponse";
import ConnectionInfoModel from "../../models/ConnectionInfoModel.ts";

/**
 * Response type for hypervisor connection information.
 */
type ConnectionInfoResponse = BaseResponse<ConnectionInfoModel>;

export default ConnectionInfoResponse;
