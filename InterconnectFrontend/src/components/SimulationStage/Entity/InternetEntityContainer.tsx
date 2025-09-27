import InternetEntityModel from "../../../models/InternetEntityModel.ts";
import InternetEntity from "./InternetEntity.tsx";
import useChangeCursor from "../../../hooks/useChangeCursor.ts";
import { KonvaEventObject } from "konva/lib/Node";
import { useInternetEntitiesStore } from "../../../store/entitiesStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useFullscreenLoader from "../../../hooks/useFullscreenLoader.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import { useMemo } from "react";
import simulationStageEntitiesUtils from "../../../utils/simulationStageEntitiesUtils.ts";

interface InternetEntityContainerProps {
    entity: InternetEntityModel;
}

const InternetEntityContainer = ({ entity }: InternetEntityContainerProps) => {
    const internetEntitiesStore = useInternetEntitiesStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const changeCursor = useChangeCursor();
    const { startLoading, stopLoading } = useFullscreenLoader();

    const handleOnMouseOver = (e: KonvaEventObject<MouseEvent>) => {
        changeCursor(e, "grab");
    };

    const handleOnMouseOut = (e: KonvaEventObject<MouseEvent>) => {
        changeCursor(e, "unset");
    };

    const handleDragEnd = (e: KonvaEventObject<DragEvent>) => {
        internetEntitiesStore.updateEntityPosition(
            entity.id,
            e.target.x(),
            e.target.y(),
            true,
        );
        changeCursor(e, "grab");
    };

    const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
        internetEntitiesStore.updateEntityPosition(
            entity.id,
            e.target.x(),
            e.target.y(),
        );
        changeCursor(e, "grabbing");
    };

    const attachVirtualNetwork = async (): Promise<boolean> => {
        if (entityPlacementStore.currentEntityType !== EntityType.Network) {
            return false;
        }
        startLoading();

        try {
            networkPlacementStore.setDestinationEntity(
                entity,
                EntityType.Internet,
            );
            await entityPlacementStore.placeCurrentEntity(0, 0);
        } finally {
            stopLoading();
        }
        return true;
    };

    const handleOnClick = async (e: KonvaEventObject<MouseEvent>) => {
        if (e.evt.button !== 0) {
            return;
        }

        await attachVirtualNetwork();
    };

    const shapeName = useMemo(() => {
        return simulationStageEntitiesUtils.createShapeName(
            { id: entity.id! },
            EntityType.Internet,
        );
    }, [entity.id]);

    return (
        <InternetEntity
            entity={entity}
            shapeName={shapeName ?? ""}
            onClick={handleOnClick}
            onMouseOver={handleOnMouseOver}
            onMouseOut={handleOnMouseOut}
            onDragEnd={handleDragEnd}
            onDragMove={handleDragMove}
        />
    );
};

export default InternetEntityContainer;
