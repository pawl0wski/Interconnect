import { BootableDiskModel } from "../../models/BootableDiskModel.ts";
import BaseResponse from "./BaseResponse.ts";

type BootableDisksResponse = BaseResponse<BootableDiskModel[]>;

export default BootableDisksResponse;
