import { Circle, Group, Image, Text } from "react-konva";
import virtualMachineImage from "../../../static/virtual_machine.svg";
import useImage from "use-image";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { KonvaEventObject } from "konva/lib/Node";

interface VirtualMachineEntityProps {
    entity: VirtualMachineEntityModel;
    onDragMove: (e: KonvaEventObject<DragEvent>) => void;
    onDragEnd: (e: KonvaEventObject<DragEvent>) => void;
    onMouseOver: (e: KonvaEventObject<MouseEvent>) => void;
    onMouseOut: (e: KonvaEventObject<MouseEvent>) => void;
    onClick: (e: KonvaEventObject<MouseEvent>) => void;
}

const VirtualMachineEntity = ({
                                  entity,
                                  onDragEnd,
                                  onDragMove,
                                  onMouseOver,
                                  onMouseOut,
                                  onClick
                              }: VirtualMachineEntityProps) => {
    const { name, x, y } = entity;
    const [virtualMachineImageElement] = useImage(virtualMachineImage);

    return <Group
        x={x}
        y={y}
        onMouseOver={onMouseOver}
        onMouseOut={onMouseOut}
        onDragMove={onDragMove}
        onDragEnd={onDragEnd}
        onClick={onClick}
        draggable={true}
    >
        <Image
            height={50}
            width={50}
            image={virtualMachineImageElement}
        />
        <Circle />
        <Text y={60} fontStyle="bold" align="center" width={50} text={name} />
    </Group>;
};

export default VirtualMachineEntity;