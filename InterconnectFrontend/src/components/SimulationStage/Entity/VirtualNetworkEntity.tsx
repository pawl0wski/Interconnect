import { Line } from "react-konva";
import { PositionModel } from "../../../models/PositionModel.ts";

interface VirtualNetworkPlacingEntityProps {
    visible: boolean;
    sourcePosition: PositionModel;
    destinationPosition: PositionModel;
}

const VirtualNetworkEntity = ({
                                         visible,
                                         sourcePosition,
                                         destinationPosition
                                     }: VirtualNetworkPlacingEntityProps) => {
    return <Line strokeWidth={2} stroke="black"
                 visible={visible}
                 points={[sourcePosition.x, sourcePosition.y, destinationPosition.x, destinationPosition.y]} />;
};

export default VirtualNetworkEntity;