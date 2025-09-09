import BaseResponse from "./BaseResponse.ts";
import InternetEntityModel from "../../models/InternetEntityModel.ts";

type InternetEntitiesResponse = BaseResponse<InternetEntityModel[]>;

export default InternetEntitiesResponse;