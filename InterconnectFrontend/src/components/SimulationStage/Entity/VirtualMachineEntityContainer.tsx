import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import VirtualMachineEntity from "./VirtualMachineEntity.tsx";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { KonvaEventObject } from "konva/lib/Node";

interface VirtualMachineEntityContainerProps {
    entity: VirtualMachineEntityModel;
}

const VirtualMachineEntityContainer = ({ entity }: VirtualMachineEntityContainerProps) => {
    const virtualMachineEntityStore = useVirtualMachineEntitiesStore();

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

    return <VirtualMachineEntity
        entity={entity}
        onMouseOver={handleOnMouseOver}
        onMouseOut={handleOnMouseOut}
        onDragEnd={handleDragEnd}
        onDragMove={handleDragMove}
    />;
};

export default VirtualMachineEntityContainer;