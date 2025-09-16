import VirtualNetworkEntity from "./VirtualNetworkEntity.tsx";
import { useEffect, useMemo, useState } from "react";
import useNetworkPlacementStore from "../../../store/networkPlacementStore.ts";
import { PositionModel } from "../../../models/PositionModel.ts";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";

const VirtualNetworkPlacingConnectionContainer = () => {
    const networkPlacementStore = useNetworkPlacementStore();
    const entityPlacementStore = useEntityPlacementStore();
    const [destinationPosition, setDestinationPosition] =
        useState<PositionModel>({ x: 0, y: 0 });
    const [sourcePosition, setSourcePosition] = useState<PositionModel>({
        x: 0,
        y: 0,
    });

    const visible = useMemo(
        () => entityPlacementStore.currentEntityType === EntityType.Network,
        [entityPlacementStore.currentEntityType],
    );

    useEffect(() => {
        if (!networkPlacementStore.sourceEntity) {
            setSourcePosition({ x: 0, y: 0 });
            return;
        }

        const { x, y } = networkPlacementStore.sourceEntity!;
        setSourcePosition({ x: x + 25, y: y + 25 });
    }, [networkPlacementStore.sourceEntity]);

    useEffect(() => {
        const handleMouseMove = (event: MouseEvent) => {
            setDestinationPosition({ x: event.layerX, y: event.layerY });
        };

        window.addEventListener("mousemove", handleMouseMove);

        return () => {
            window.removeEventListener("mousemove", handleMouseMove);
        };
    }, []);

    return (
        <>
            {entityPlacementStore.currentEntityType && (
                <VirtualNetworkEntity
                    visible={visible}
                    destinationPosition={destinationPosition}
                    sourcePosition={sourcePosition}
                />
            )}
        </>
    );
};

export default VirtualNetworkPlacingConnectionContainer;
