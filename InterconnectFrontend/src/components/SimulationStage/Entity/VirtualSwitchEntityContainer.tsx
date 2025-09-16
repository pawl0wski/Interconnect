import VirtualSwitchEntityModel from "../../../models/VirtualSwitchEntityModel.ts";
import VirtualSwitchEntity from "./VirtualSwitchEntity.tsx";
import { KonvaEventObject } from "konva/lib/Node";
import { useVirtualSwitchEntitiesStore } from "../../../store/entitiesStore.ts";
import useChangeCursor from "../../../hooks/useChangeCursor.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";
import simulationStageEntitiesUtils from "../../../utils/simulationStageEntitiesUtils.ts";
import useFullscreenLoader from "../../../hooks/useFullscreenLoader.ts";

interface VirtualSwitchEntityContainerProps {
    entity: VirtualSwitchEntityModel;
}

const VirtualSwitchEntityContainer = ({
    entity,
}: VirtualSwitchEntityContainerProps) => {
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();
    const entityPlacementStore = useEntityPlacementStore();
    const networkPlacementStore = useNetworkPlacementStore();
    const { startLoading, stopLoading } = useFullscreenLoader();

    const shapeName = useMemo(() => {
        return simulationStageEntitiesUtils.createShapeName(
            { id: entity.id! },
            EntityType.VirtualSwitch,
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
        virtualSwitchEntitiesStore.updateEntityPosition(
            entity.id,
            e.target.x(),
            e.target.y(),
            true,
        );
        changeCursor(e, "grab");
    };

    const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
        virtualSwitchEntitiesStore.updateEntityPosition(
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
                EntityType.VirtualSwitch,
            );
            await entityPlacementStore.placeCurrentEntity(0, 0);
        } finally {
            stopLoading();
        }
        return true;
    };

    return (
        <VirtualSwitchEntity
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

export default VirtualSwitchEntityContainer;
