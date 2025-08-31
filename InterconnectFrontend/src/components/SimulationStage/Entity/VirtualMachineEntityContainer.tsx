import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import VirtualMachineEntity from "./VirtualMachineEntity.tsx";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { KonvaEventObject } from "konva/lib/Node";
import { useCurrentVirtualMachineModalStore } from "../../../store/modals/currentVirtualMachineModalStore.ts";
import { useCurrentVirtualMachineStore } from "../../../store/currentVirtualMachineStore.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { useMemo } from "react";
import simulationStageEntitiesUtils from "../../../utils/simulationStageEntitiesUtils.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";

interface VirtualMachineEntityContainerProps {
    entity: VirtualMachineEntityModel;
}

const VirtualMachineEntityContainer = ({ entity }: VirtualMachineEntityContainerProps) => {
        const virtualMachineEntityStore = useVirtualMachineEntitiesStore();
        const currentVirtualMachineStore = useCurrentVirtualMachineStore();
        const currentVirtualMachineModalStore = useCurrentVirtualMachineModalStore();
        const entityPlacementStore = useEntityPlacementStore();

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
            virtualMachineEntityStore.updateEntityPosition(entity.id, e.target.x(), e.target.y());
            changeCursor(e, "pointer");
        };

        const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
            changeCursor(e, "move");
        };

        const handleOnClick = (e: KonvaEventObject<MouseEvent>) => {
            if (e.evt.button !== 0) {
                return;
            }

            currentVirtualMachineStore.setCurrentEntity(entity);
            currentVirtualMachineModalStore.open();
        };

        return <VirtualMachineEntity
            entity={entity}
            shapeName={shapeName ?? ""}
            onMouseOver={handleOnMouseOver}
            onMouseOut={handleOnMouseOut}
            onDragEnd={handleDragEnd}
            onDragMove={handleDragMove}
            onClick={handleOnClick}
        />;
    }
;

export default VirtualMachineEntityContainer;