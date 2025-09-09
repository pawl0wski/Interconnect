import InternetEntityModel from "../../../models/InternetEntityModel.ts";
import InternetEntity from "./InternetEntity.tsx";
import useChangeCursor from "../../../hooks/useChangeCursor.ts";
import { KonvaEventObject } from "konva/lib/Node";
import { useInternetEntitiesStore } from "../../../store/entitiesStore.ts";

interface InternetEntityContainerProps {
    entity: InternetEntityModel;
}

const InternetEntityContainer = ({ entity }: InternetEntityContainerProps) => {
    const internetEntitiesStore = useInternetEntitiesStore();
    const changeCursor = useChangeCursor();

    const handleOnMouseOver = (e: KonvaEventObject<MouseEvent>) => {
        changeCursor(e, "grab");
    };

    const handleOnMouseOut = (e: KonvaEventObject<MouseEvent>) => {
        changeCursor(e, "unset");
    };

    const handleDragEnd = (e: KonvaEventObject<DragEvent>) => {
        internetEntitiesStore.updateEntityPosition(entity.id, e.target.x(), e.target.y(), true);
        changeCursor(e, "grab");
    };

    const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
        internetEntitiesStore.updateEntityPosition(entity.id, e.target.x(), e.target.y());
        changeCursor(e, "grabbing");
    };

    return <InternetEntity entity={entity}
                           onMouseOver={handleOnMouseOver}
                           onMouseOut={handleOnMouseOut}
                           onDragEnd={handleDragEnd}
                           onDragMove={handleDragMove}
    />;
};

export default InternetEntityContainer;