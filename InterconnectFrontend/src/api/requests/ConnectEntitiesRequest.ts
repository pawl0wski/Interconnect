import BaseRequest from "./BaseRequest.ts";
import { EntityType } from "../../models/enums/EntityType.ts";

/**
 * Request object for creating a connection between two entities.
 */
interface ConnectEntitiesRequest extends BaseRequest {
    /** ID of the source entity */
    sourceEntityId: number;
    /** Type of the source entity */
    sourceEntityType: EntityType;
    /** ID of the destination entity */
    destinationEntityId: number;
    /** Type of the destination entity */
    destinationEntityType: EntityType;
}

export default ConnectEntitiesRequest;
