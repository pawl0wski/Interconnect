import { create } from "zustand/react";
import BaseEntity from "../models/interfaces/BaseEntity.ts";
import { EntityType } from "../models/enums/EntityType.ts";
import ConnectEntitiesRequest from "../api/requests/ConnectEntitiesRequest.ts";

interface NetworkPlacementStore {
    sourceEntity: BaseEntity | null;
    sourceEntityType: EntityType | null;
    sourceSocketId: number | null;
    destinationEntity: BaseEntity | null;
    destinationEntityType: EntityType | null;
    destinationSocketId: number | null;
    setSourceEntity: (sourceEntity: BaseEntity, sourceEntityType: EntityType, sourceSocketId: number) => void;
    setDestinationEntity: (destinationEntity: BaseEntity, destinationEntityType: EntityType, destinationSocketId: number) => void;
    combineAsRequest: () => ConnectEntitiesRequest;
    clear: () => void;
}

const useNetworkPlacementStore = create<NetworkPlacementStore>()((set, get) => ({
    sourceEntity: null,
    sourceEntityType: null,
    sourceSocketId: null,
    destinationEntity: null,
    destinationEntityType: null,
    destinationSocketId: null,
    setSourceEntity: (sourceEntity: BaseEntity, sourceEntityType: EntityType, sourceSocketId: number) => set({
        sourceEntity,
        sourceEntityType,
        sourceSocketId
    }),
    setDestinationEntity: (destinationEntity: BaseEntity, destinationEntityType: EntityType, destinationSocketId: number) => set({
        destinationEntity,
        destinationEntityType,
        destinationSocketId
    }),
    combineAsRequest: (): ConnectEntitiesRequest => {
        const {
            sourceEntity,
            sourceEntityType,
            sourceSocketId,
            destinationEntity,
            destinationEntityType,
            destinationSocketId
        } = get();

        return {
            sourceEntity: sourceEntity!,
            sourceEntityType: sourceEntityType!,
            sourceSocketId: sourceSocketId!,
            destinationEntity: destinationEntity!,
            destinationEntityType: destinationEntityType!,
            destinationSocketId: destinationSocketId!
        };
    },
    clear: () => set({ sourceEntity: null })
}));

export default useNetworkPlacementStore;