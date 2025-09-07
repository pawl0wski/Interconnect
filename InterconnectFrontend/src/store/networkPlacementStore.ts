import { create } from "zustand/react";
import BaseEntity from "../models/interfaces/BaseEntity.ts";
import { EntityType } from "../models/enums/EntityType.ts";
import ConnectEntitiesRequest from "../api/requests/ConnectEntitiesRequest.ts";

interface NetworkPlacementStore {
    sourceEntity: BaseEntity | null;
    sourceEntityType: EntityType | null;
    destinationEntity: BaseEntity | null;
    destinationEntityType: EntityType | null;
    setSourceEntity: (sourceEntity: BaseEntity, sourceEntityType: EntityType) => void;
    setDestinationEntity: (destinationEntity: BaseEntity, destinationEntityType: EntityType) => void;
    combineAsRequest: () => ConnectEntitiesRequest;
    clear: () => void;
}

const useNetworkPlacementStore = create<NetworkPlacementStore>()((set, get) => ({
    sourceEntity: null,
    sourceEntityType: null,
    destinationEntity: null,
    destinationEntityType: null,
    setSourceEntity: (sourceEntity: BaseEntity, sourceEntityType: EntityType) => set({
        sourceEntity,
        sourceEntityType,
    }),
    setDestinationEntity: (destinationEntity: BaseEntity, destinationEntityType: EntityType) => set({
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
    clear: () => set({ sourceEntity: null })
}));

export default useNetworkPlacementStore;