import { create } from "zustand/react";
import BaseEntity from "../models/interfaces/BaseEntity.ts";
import { EntityType } from "../models/enums/EntityType.ts";
import ConnectEntitiesRequest from "../api/requests/ConnectEntitiesRequest.ts";

/**
 * State store for managing network connection placement (creating connections between entities).
 * Tracks the source and destination entities for a new network connection.
 */
interface NetworkPlacementStore {
    /** The source entity for the connection being created, or null */
    sourceEntity: BaseEntity | null;
    /** The entity type of the source, or null */
    sourceEntityType: EntityType | null;
    /** The destination entity for the connection being created, or null */
    destinationEntity: BaseEntity | null;
    /** The entity type of the destination, or null */
    destinationEntityType: EntityType | null;
    /** Sets the source entity and its type */
    setSourceEntity: (
        sourceEntity: BaseEntity,
        sourceEntityType: EntityType,
    ) => void;
    /** Sets the destination entity and its type */
    setDestinationEntity: (
        destinationEntity: BaseEntity,
        destinationEntityType: EntityType,
    ) => void;
    /** Combines current source and destination into a ConnectEntitiesRequest */
    combineAsRequest: () => ConnectEntitiesRequest;
    /** Clears both source and destination entities */
    clear: () => void;
}

/**
 * Zustand store hook for managing the network placement workflow.
 * Stores temporary state for creating connections between two entities.
 */
const useNetworkPlacementStore = create<NetworkPlacementStore>()(
    (set, get) => ({
        sourceEntity: null,
        sourceEntityType: null,
        destinationEntity: null,
        destinationEntityType: null,
        setSourceEntity: (
            sourceEntity: BaseEntity,
            sourceEntityType: EntityType,
        ) =>
            set({
                sourceEntity,
                sourceEntityType,
            }),
        setDestinationEntity: (
            destinationEntity: BaseEntity,
            destinationEntityType: EntityType,
        ) =>
            set({
                destinationEntity,
                destinationEntityType,
            }),
        combineAsRequest: (): ConnectEntitiesRequest => {
            const {
                sourceEntity,
                sourceEntityType,
                destinationEntity,
                destinationEntityType,
            } = get();

            return {
                sourceEntityId: sourceEntity!.id!,
                sourceEntityType: sourceEntityType!,
                destinationEntityId: destinationEntity!.id!,
                destinationEntityType: destinationEntityType!,
            };
        },
        clear: () => set({ sourceEntity: null, destinationEntity: null }),
    }),
);

export default useNetworkPlacementStore;
