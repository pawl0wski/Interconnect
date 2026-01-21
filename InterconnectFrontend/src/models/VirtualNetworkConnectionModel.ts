import { EntityType } from "./enums/EntityType.ts";

/**
 * Represents a connection between two entities in the virtual network.
 */
interface VirtualNetworkConnectionModel {
    /** Unique identifier for the connection */
    id: number;
    /** ID of the source entity */
    sourceEntityId: number;
    /** Type of the source entity */
    sourceEntityType: EntityType;
    /** ID of the destination entity */
    destinationEntityId: number;
    /** Type of the destination entity */
    destinationEntityType: EntityType;
}

export default VirtualNetworkConnectionModel;
