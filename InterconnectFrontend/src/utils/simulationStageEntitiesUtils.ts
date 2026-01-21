import { EntityType } from "../models/enums/EntityType.ts";
import { ObjectWithName } from "../models/interfaces/ObjectWithName.ts";
import { ObjectWithId } from "../models/interfaces/ObjectWithId.ts";

/**
 * Represents an entity with its type and ID information.
 */
interface EntityTypeWithId {
    id: number;
    type: EntityType;
}

/**
 * Utility class for managing and parsing simulation stage entity representations.
 * Handles entity naming conventions and shape hierarchy navigation.
 */
const SimulationStageEntitiesUtils = {
    /**
     * Retrieves the name from a shape object by traversing its parent hierarchy if needed.
     * Useful for extracting entity names from nested shape structures.
     * @param {ObjectWithName} shape The shape object to extract the name from
     * @returns {string | null} The entity name, or null if no name is found in the hierarchy
     */
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
    /**
     * Creates a shape name for an entity based on its type and ID.
     * Naming convention: {type-abbreviation}-{id} (e.g., "vm-5", "sw-3")
     * @param {ObjectWithId} entity The entity object with an ID
     * @param {EntityType} entityType The type of entity
     * @returns {string} The generated shape name
     */
    createShapeName(entity: ObjectWithId, entityType: EntityType) {
        switch (entityType) {
            case EntityType.VirtualMachine:
                return `vm-${entity.id}`;
            case EntityType.VirtualNetworkNode:
                return `sw-${entity.id}`;
            case EntityType.Internet:
                return `in-${entity.id}`;
        }
    },
    /**
     * Parses a shape name to extract the entity type and ID.
     * Reverses the format created by createShapeName().
     * @param {string} name The shape name to parse (e.g., "vm-5")
     * @returns {EntityTypeWithId | null} The parsed entity type and ID, or null if parsing fails
     */
    parseShapeName(name: string): EntityTypeWithId | null {
        const [typeName, id] = name.split("-");
        let entityType: EntityType | null = null;
        switch (typeName) {
            case "vm":
                entityType = EntityType.VirtualMachine;
                break;
            case "sw":
                entityType = EntityType.VirtualNetworkNode;
                break;
            case "in":
                entityType = EntityType.Internet;
                break;
        }

        if (!entityType) {
            return null;
        }

        return { type: entityType, id: parseInt(id) };
    },
};

export default SimulationStageEntitiesUtils;
