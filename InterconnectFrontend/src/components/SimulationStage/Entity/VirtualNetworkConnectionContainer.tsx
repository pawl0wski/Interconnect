import { useMemo } from "react";
import VirtualNetworkConnection from "./VirtualNetworkEntity.tsx";
import VirtualNetworkConnectionModel from "../../../models/VirtualNetworkConnectionModel.ts";
import { useEntityPosition } from "../../../hooks/useEntityPosition.ts";

interface VirtualNetworkConnectionContainerProps {
    virtualNetworkEntity: VirtualNetworkConnectionModel;
}

const VirtualNetworkConnectionContainer = ({
    virtualNetworkEntity,
}: VirtualNetworkConnectionContainerProps) => {
    const sourceEntityPosition = useEntityPosition(
        virtualNetworkEntity.sourceEntityId,
        virtualNetworkEntity.sourceEntityType,
    );
    const destinationEntityPosition = useEntityPosition(
        virtualNetworkEntity.destinationEntityId,
        virtualNetworkEntity.destinationEntityType,
    );

    const sourcePosition = useMemo(
        () => ({
            x: sourceEntityPosition.x + 25,
            y: sourceEntityPosition.y + 25,
        }),
        [sourceEntityPosition.x, sourceEntityPosition.y],
    );

    const destinationPosition = useMemo(
        () => ({
            x: destinationEntityPosition.x + 25,
            y: destinationEntityPosition.y + 25,
        }),
        [destinationEntityPosition.x, destinationEntityPosition.y],
    );

    if (
        (sourceEntityPosition.x === 0 && sourceEntityPosition.y === 0) ||
        (destinationEntityPosition.y === 0 && destinationEntityPosition.y === 0)
    ) {
        return null;
    }
    
    return (
        sourcePosition &&
        destinationPosition && (
            <VirtualNetworkConnection
                visible
                sourcePosition={sourcePosition}
                destinationPosition={destinationPosition}
            />
        )
    );
};

export default VirtualNetworkConnectionContainer;
