import InternetEntityModel from "../../../models/InternetEntityModel.ts";
import { Circle, Group, Image, Text } from "react-konva";
import internetActive from "../../../static/internetActive.svg";
import useImage from "use-image";
import { KonvaEventObject } from "konva/lib/Node";

interface InternetEntityProps {
    entity: InternetEntityModel;
    shapeName: string;
    onClick: (e: KonvaEventObject<MouseEvent>) => void;
    onDragMove: (e: KonvaEventObject<DragEvent>) => void;
    onDragEnd: (e: KonvaEventObject<DragEvent>) => void;
    onMouseOver: (e: KonvaEventObject<MouseEvent>) => void;
    onMouseOut: (e: KonvaEventObject<MouseEvent>) => void;
}

const InternetEntity = ({
    entity,
    shapeName,
    onClick,
    onDragMove,
    onDragEnd,
    onMouseOver,
    onMouseOut,
}: InternetEntityProps) => {
    const [internetActiveElement] = useImage(internetActive);

    return (
        <Group
            x={entity.x}
            y={entity.y}
            name={shapeName}
            onClick={onClick}
            onDragMove={onDragMove}
            onDragEnd={onDragEnd}
            onMouseOver={onMouseOver}
            onMouseOut={onMouseOut}
            draggable
        >
            <Image height={50} width={50} image={internetActiveElement} />
            <Circle />
            <Text
                y={60}
                fontStyle="bold"
                align="center"
                width={50}
                text="Internet"
            />
        </Group>
    );
};

export default InternetEntity;
