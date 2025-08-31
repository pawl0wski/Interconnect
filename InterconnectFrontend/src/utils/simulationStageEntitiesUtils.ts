import { EntityType } from "../models/enums/EntityType.ts";
import { ObjectWithName } from "../models/interfaces/ObjectWithName.ts";
import { ObjectWithId } from "../models/interfaces/ObjectWithId.ts";

interface EntityTypeWithId {
    id: number;
    type: EntityType,
}

const SimulationStageEntitiesUtils = {
    getTargetOrParentEntityInfo(shape: ObjectWithName): string | null {
        let currentShape: ObjectWithName | null = shape;

        while (currentShape) {
            const name = currentShape.name?.();
            if (name) {
                return name;
            }
            currentShape = currentShape.parent ?? null;
        }

        return null;
    },
    createShapeName(entity: ObjectWithId, entityType: EntityType) {
        switch (entityType) {
            case EntityType.VirtualMachine:
                return `vm-${entity.id}`;
        }
    },
    parseShapeName(name: string): EntityTypeWithId | null {
        const [typeName, id] = name.split("-");
        let entityType: EntityType | null = null;
        switch (typeName) {
            case "vm":
                entityType = EntityType.VirtualMachine;
                break;
        }

        if (!entityType) {
            return null;
        }

        return { type: entityType, id: parseInt(id) };
    }

};

export default SimulationStageEntitiesUtils;