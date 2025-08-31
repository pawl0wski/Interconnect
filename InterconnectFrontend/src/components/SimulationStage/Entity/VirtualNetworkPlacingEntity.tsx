import { Line } from "react-konva";
import { PositionModel } from "../../../models/PositionModel.ts";

interface VirtualNetworkPlacingEntityProps {
    sourcePosition: PositionModel;
    destinationPosition: PositionModel;
}

const VirtualNetworkPlacingEntity = ({ sourcePosition, destinationPosition }: VirtualNetworkPlacingEntityProps) => {
    return <Line strokeWidth={2} stroke="black" points={[sourcePosition.x, sourcePosition.y, destinationPosition.x, destinationPosition.y]} />;
};

export default VirtualNetworkPlacingEntity;