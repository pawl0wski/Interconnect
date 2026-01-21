import { TerminalDataModel } from "../../models/TerminalDataModel.ts";
import BaseResponse from "./BaseResponse.ts";

/**
 * Response type for terminal output data from a virtual machine console.
 */
type TerminalDataResponse = BaseResponse<TerminalDataModel>;

export default TerminalDataResponse;
