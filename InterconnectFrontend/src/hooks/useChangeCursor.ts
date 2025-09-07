import { KonvaEventObject } from "konva/lib/Node";
import { useEntityPlacementStore } from "../store/entityPlacementStore.ts";

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