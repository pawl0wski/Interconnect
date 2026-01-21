import { useMemo } from "react";
import { useEntityPlacementStore } from "../store/entityPlacementStore.ts";

/**
 * Custom hook that determines whether entities in the simulation stage can be dragged.
 * Returns false if an entity is currently being placed.
 * @returns {boolean} True if entities can be dragged, false otherwise
 */
const useIsEntityDraggable = () => {
    const entityPlacementStore = useEntityPlacementStore();

    return useMemo(
        () => !entityPlacementStore.currentEntityType,
        [entityPlacementStore.currentEntityType],
    );
};

export default useIsEntityDraggable;
