import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import VirtualMachineEntity from "./VirtualMachineEntity.tsx";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";

interface VirtualMachineEntityContainerProps {
    entity: VirtualMachineEntityModel;
}

const VirtualMachineEntityContainer = ({ entity }: VirtualMachineEntityContainerProps) => {
    const virtualMachineEntityStore = useVirtualMachineEntitiesStore();

    const handleDragEnd = (x: number, y: number) => {
        virtualMachineEntityStore.updateEntityPosition(entity.id, x, y);
    };

    return <VirtualMachineEntity entity={entity} onDragEnd={handleDragEnd} />;
};

export default VirtualMachineEntityContainer;