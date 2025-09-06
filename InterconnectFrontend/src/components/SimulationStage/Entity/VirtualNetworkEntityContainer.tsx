import VirtualNetworkEntityConnectionModel from "../../../models/VirtualNetworkEntityConnectionModel.ts";
import VirtualNetworkEntity from "./VirtualNetworkEntity.tsx";
import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import { useCallback, useMemo } from "react";
import { PositionModel } from "../../../models/PositionModel.ts";

interface VirtualNetworkEntityContainerProps {
    virtualNetworkEntity: VirtualNetworkEntityConnectionModel;
}

const VirtualNetworkEntityContainer = ({ virtualNetworkEntity }: VirtualNetworkEntityContainerProps) => {
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();

    const getEntityPosition = useCallback((uuid: string): PositionModel | null => {
        const entity = virtualMachineEntitiesStore.getByVmUuid(uuid);

        if (!entity) {
            return null;
        }

        return { x: entity.x + 25, y: entity.y + 25 };
    }, [virtualMachineEntitiesStore]);

    const sourcePosition = useMemo(() =>
            getEntityPosition(virtualNetworkEntity.firstEntityUuid),
        [getEntityPosition, virtualNetworkEntity.firstEntityUuid]);

    const destinationPosition = useMemo(() =>
            getEntityPosition(virtualNetworkEntity.secondEntityUuid),
        [getEntityPosition, virtualNetworkEntity.secondEntityUuid]);

    return (sourcePosition && destinationPosition) &&
        <VirtualNetworkEntity visible sourcePosition={sourcePosition} destinationPosition={destinationPosition} />;
};

export default VirtualNetworkEntityContainer;
