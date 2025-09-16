import { Circle, Group, Image, Text } from "react-konva";
import useImage from "use-image";
import { KonvaEventObject } from "konva/lib/Node";
import virtualSwitchImage from "../../../static/switchActive.svg";
import VirtualSwitchEntityModel from "../../../models/VirtualSwitchEntityModel.ts";

interface VirtualSwitchEntityProps {
    entity: VirtualSwitchEntityModel;
    shapeName: string;
    onDragMove: (e: KonvaEventObject<DragEvent>) => void;
    onDragEnd: (e: KonvaEventObject<DragEvent>) => void;
    onMouseOver: (e: KonvaEventObject<MouseEvent>) => void;
    onMouseOut: (e: KonvaEventObject<MouseEvent>) => void;
    onClick: (e: KonvaEventObject<MouseEvent>) => void;
}

const VirtualSwitchEntity = ({
    entity,
    shapeName,
    onDragMove,
    onDragEnd,
    onMouseOut,
    onMouseOver,
    onClick,
}: VirtualSwitchEntityProps) => {
    const [virtualSwitchImageElement] = useImage(virtualSwitchImage);

    return (
        <Group
            draggable
            name={shapeName}
            x={entity.x}
            y={entity.y}
            onDragMove={onDragMove}
            onDragEnd={onDragEnd}
            onMouseOver={onMouseOver}
            onMouseOut={onMouseOut}
            onClick={onClick}
        >
            <Image height={50} width={50} image={virtualSwitchImageElement} />
            <Circle />
            <Text
                y={60}
                fontStyle="bold"
                align="center"
                width={50}
                text={entity.name ?? ""}
            />
        </Group>
    );
};

export default VirtualSwitchEntity;
