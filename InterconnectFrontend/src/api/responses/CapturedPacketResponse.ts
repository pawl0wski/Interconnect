import BaseResponse from "./BaseResponse.ts";
import PacketModel from "../../models/PacketModel.ts";

/**
 * Response type for a single captured network packet.
 */
type CapturedPacketResponse = BaseResponse<PacketModel>;

export default CapturedPacketResponse;
