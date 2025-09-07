import BaseRequest from "./BaseRequest.ts";
import { EntityType } from "../../models/enums/EntityType.ts";

interface ConnectEntitiesRequest extends BaseRequest {
    sourceEntityId: number;
    sourceEntityType: EntityType;
    destinationEntityId: number;
    destinationEntityType: EntityType;
}

export default ConnectEntitiesRequest;