import { EntityType } from "../../models/enums/EntityType.ts";

interface DeleteEntityRequest {
    id: number;
    type: EntityType;
}

export default DeleteEntityRequest;