import { useMemo } from "react";
import { useEntityPlacementStore } from "../store/entityPlacementStore.ts";

const useIsEntityDraggable = () => {
    const entityPlacementStore = useEntityPlacementStore();

    return useMemo(
        () => !entityPlacementStore.currentEntityType,
        [entityPlacementStore.currentEntityType],
    );
};

export default useIsEntityDraggable;
