import { BootableDiskModel } from "../../models/BootableDiskModel.ts";
import BaseResponse from "./BaseResponse.ts";

/**
 * Response type for a list of available bootable disks.
 */
type BootableDisksResponse = BaseResponse<BootableDiskModel[]>;

export default BootableDisksResponse;
