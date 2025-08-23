import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import VirtualMachineEntity from "./VirtualMachineEntity.tsx";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { KonvaEventObject } from "konva/lib/Node";
import { useCurrentVirtualMachineModalStore } from "../../../store/modals/currentVirtualMachineModalStore.ts";
import { useCurrentVirtualMachineStore } from "../../../store/currentVirtualMachineStore.ts";

interface VirtualMachineEntityContainerProps {
    entity: VirtualMachineEntityModel;
}

const VirtualMachineEntityContainer = ({ entity }: VirtualMachineEntityContainerProps) => {
    const virtualMachineEntityStore = useVirtualMachineEntitiesStore();
    const currentVirtualMachineStore = useCurrentVirtualMachineStore();
    const currentVirtualMachineModalStore = useCurrentVirtualMachineModalStore();

    const changeCursor = (e: KonvaEventObject<any>, cursor: string) => {
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
        changeCursor(e, "default");
    };

    const handleDragEnd = (e: KonvaEventObject<DragEvent>) => {
        virtualMachineEntityStore.updateEntityPosition(entity.id, e.target.x(), e.target.y());
        changeCursor(e, "pointer");
    };

    const handleDragMove = (e: KonvaEventObject<DragEvent>) => {
        changeCursor(e, "move");
    };

    const handleOnClick = () => {
        currentVirtualMachineStore.setUuid(entity.vmUuid!);
        currentVirtualMachineModalStore.open();
    };

    return <VirtualMachineEntity
        entity={entity}
        onMouseOver={handleOnMouseOver}
        onMouseOut={handleOnMouseOut}
        onDragEnd={handleDragEnd}
        onDragMove={handleDragMove}
        onClick={handleOnClick}
    />;
};

export default VirtualMachineEntityContainer;