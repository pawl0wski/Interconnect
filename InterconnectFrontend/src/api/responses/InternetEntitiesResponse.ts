import BaseResponse from "./BaseResponse.ts";
import InternetEntityModel from "../../models/InternetEntityModel.ts";

/**
 * Response type for a list of Internet entities.
 */
type InternetEntitiesResponse = BaseResponse<InternetEntityModel[]>;

export default InternetEntitiesResponse;
