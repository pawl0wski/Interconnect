import { EntityType } from "./enums/EntityType.ts";

interface VirtualNetworkConnectionModel {
    id: number;
    sourceEntityId: number;
    sourceEntityType: EntityType;
    destinationEntityId: number;
    destinationEntityType: EntityType;
}

export default VirtualNetworkConnectionModel;
