import { EntityType } from "../../models/enums/EntityType.ts";

/**
 * Request object for updating the position of an entity in the simulation.
 */
interface UpdateEntityPositionRequest {
    /** ID of the entity to move */
    id: number;
    /** Type of the entity */
    type: EntityType;
    /** New X coordinate */
    x: number;
    /** New Y coordinate */
    y: number;
}

export default UpdateEntityPositionRequest;
