import VirtualNetworkNodeEntityModel from "../../../models/VirtualNetworkNodeEntityModel.ts";
import VirtualNetworkNodeEntity from "./VirtualNetworkNodeEntity.tsx";
import { KonvaEventObject } from "konva/lib/Node";
import { useVirtualNetworkNodeEntitiesStore } from "../../../store/entitiesStore.ts";
import useChangeCursor from "../../../hooks/useChangeCursor.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";
import simulationStageEntitiesUtils from "../../../utils/simulationStageEntitiesUtils.ts";
import useFullscreenLoader from "../../../hooks/useFullscreenLoader.ts";

interface VirtualNetworkNodeEntityContainerProps {
    entity: VirtualNetworkNodeEntityModel;
}

const VirtualNetworkNodeEntityContainer = ({
    entity,
}: VirtualNetworkNodeEntityContainerProps) => {
    const virtualNetworkNodeEntitiesStore = useVirtualNetworkNodeEntitiesStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const { startLoading, stopLoading } = useFullscreenLoader();

    const shapeName = useMemo(() => {
        return simulationStageEntitiesUtils.createShapeName(
            { id: entity.id! },
            EntityType.VirtualNetworkNode,
        );
    }, [entity.id]);

    const changeCursor = useChangeCursor();

    const handleOnMouseOver = (e: KonvaEventObject<MouseEvent>) => {
        changeCursor(e, "grab");
    };

    const handleOnMouseOut = (e: KonvaEventObject<MouseEvent>) => {
        changeCursor(e, "unset");
    };

    const handleDragEnd = (e: KonvaEventObject<DragEvent>) => {
        virtualNetworkNodeEntitiesStore.updateEntityPosition(
            entity.id,
            e.target.x(),
            e.target.y(),
            true,
        );
        changeCursor(e, "grab");
    };

    const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
        virtualNetworkNodeEntitiesStore.updateEntityPosition(
            entity.id,
            e.target.x(),
            e.target.y(),
        );
        changeCursor(e, "grabbing");
    };

    const handleOnClick = async (_: KonvaEventObject<MouseEvent>) => {
        if (await attachVirtualNetwork()) {
            return;
        }
    };

    const attachVirtualNetwork = async (): Promise<boolean> => {
        if (entityPlacementStore.currentEntityType !== EntityType.Network) {
            return false;
        }
        startLoading();

        try {
            networkPlacementStore.setDestinationEntity(
                entity,
                EntityType.VirtualNetworkNode,
            );
            await entityPlacementStore.placeCurrentEntity(0, 0);
        } finally {
            stopLoading();
        }
        return true;
    };

    return (
        <VirtualNetworkNodeEntity
            entity={entity}
            shapeName={shapeName ?? ""}
            onDragEnd={handleDragEnd}
            onDragMove={handleDragMove}
            onMouseOut={handleOnMouseOut}
            onMouseOver={handleOnMouseOver}
            onClick={handleOnClick}
        />
    );
};

export default VirtualNetworkNodeEntityContainer;
