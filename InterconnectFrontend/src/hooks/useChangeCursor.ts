import { KonvaEventObject } from "konva/lib/Node";
import { useEntityPlacementStore } from "../store/entityPlacementStore.ts";

/**
 * Custom hook that provides a handler for changing the cursor style on Konva canvas elements.
 * Only changes cursor if no entity is currently being placed.
 * @returns {Function} A handler function that accepts Konva event and cursor style
 */
const useChangeCursor = () => {
    const entityPlacementStore = useEntityPlacementStore();

    return (e: KonvaEventObject<any>, cursor: string) => {
        if (entityPlacementStore.currentEntityType) {
            return;
        }

        const stage = e.target.getStage();
        if (!stage) {
            return;
        }
        stage.container().style.cursor = cursor;
    };
};

export default useChangeCursor;
