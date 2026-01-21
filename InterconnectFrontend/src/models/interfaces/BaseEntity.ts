/**
 * Base interface for all entities in the simulation with position information.
 */
interface BaseEntity {
    /** Unique identifier for the entity */
    id: number;
    /** X coordinate position in the simulation stage */
    x: number;
    /** Y coordinate position in the simulation stage */
    y: number;
}

export default BaseEntity;
