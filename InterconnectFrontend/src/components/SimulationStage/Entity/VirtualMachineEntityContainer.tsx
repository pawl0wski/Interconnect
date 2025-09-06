import { useMemo } from "react";
import { KonvaEventObject } from "konva/lib/Node";
import VirtualMachineEntity from "./VirtualMachineEntity.tsx";
import simulationStageEntitiesUtils from "../../../utils/simulationStageEntitiesUtils.ts";
import useIsEntityDraggable from "../../../hooks/useIsEntityDraggable.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { useCurrentVirtualMachineModalStore } from "../../../store/modals/currentVirtualMachineModalStore.ts";
import { useCurrentVirtualMachineStore } from "../../../store/currentVirtualMachineStore.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";

interface VirtualMachineEntityContainerProps {
    entity: VirtualMachineEntityModel;
}

const VirtualMachineEntityContainer = ({ entity }: VirtualMachineEntityContainerProps) => {
        const virtualMachineEntityStore = useVirtualMachineEntitiesStore();
        const currentVirtualMachineStore = useCurrentVirtualMachineStore();
        const currentVirtualMachineModalStore = useCurrentVirtualMachineModalStore();
        const entityPlacementStore = useEntityPlacementStore();
        const networkPlacementStore = useNetworkPlacementStore();

        const draggable = useIsEntityDraggable();

        const shapeName = useMemo(() => {
            return simulationStageEntitiesUtils.createShapeName({ id: entity.id! }, EntityType.VirtualMachine);
        }, [entity.id]);

        const changeCursor = (e: KonvaEventObject<any>, cursor: string) => {
            if (entityPlacementStore.currentEntityType) {
                return;
            }

            const stage = e.target.getStage();
            if (!stage) {
                return;
            }
            stage.container().style.cursor = cursor;
        };

        const handleOnMouseOver = (e: KonvaEventObject<MouseEvent>) => {
            changeCursor(e, "pointer");
        };

        const handleOnMouseOut = (e: KonvaEventObject<MouseEvent>) => {
            changeCursor(e, "unset");
        };

        const handleDragEnd = (e: KonvaEventObject<DragEvent>) => {
            virtualMachineEntityStore.updateEntityPosition(entity.id, e.target.x(), e.target.y(), true);
            changeCursor(e, "pointer");
        };

        const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
            virtualMachineEntityStore.updateEntityPosition(entity.id, e.target.x(), e.target.y());
            changeCursor(e, "move");
        };

        const openVirtualMachineModal = () => {
            currentVirtualMachineStore.setCurrentEntity(entity);
            currentVirtualMachineModalStore.open();
        };

        const attachVirtualNetwork = async (): Promise<boolean> => {
            if (entityPlacementStore.currentEntityType !== EntityType.Network) {
                return false;
            }

            networkPlacementStore.setDestinationEntity(entity, EntityType.VirtualMachine, 1);
            await entityPlacementStore.placeCurrentEntity(0, 0);
            return true;
        };

        const handleOnClick = async (e: KonvaEventObject<MouseEvent>) => {
            if (e.evt.button !== 0) {
                return;
            }
            if (await attachVirtualNetwork()) {
                return;
            }
            openVirtualMachineModal();
        };

        return <VirtualMachineEntity
            entity={entity}
            shapeName={shapeName ?? ""}
            draggable={draggable}
            onMouseOver={handleOnMouseOver}
            onMouseOut={handleOnMouseOut}
            onDragEnd={handleDragEnd}
            onDragMove={handleDragMove}
            onClick={handleOnClick}
        />;
    }
;

export default VirtualMachineEntityContainer;