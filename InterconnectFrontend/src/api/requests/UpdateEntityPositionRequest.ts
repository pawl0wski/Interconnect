import { EntityType } from "../../models/enums/EntityType.ts";

interface UpdateEntityPositionRequest {
    id: number;
    type: EntityType;
    x: number;
    y: number;
}

export default UpdateEntityPositionRequest;
