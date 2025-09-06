import BaseRequest from "./BaseRequest.ts";
import BaseEntity from "../../models/interfaces/BaseEntity.ts";
import { EntityType } from "../../models/enums/EntityType.ts";

interface ConnectEntitiesRequest extends BaseRequest {
    sourceEntity: BaseEntity;
    sourceEntityType: EntityType;
    sourceSocketId: number;
    destinationEntity: BaseEntity;
    destinationEntityType: EntityType;
    destinationSocketId: number;
}

export default ConnectEntitiesRequest;