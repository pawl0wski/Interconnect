import { Circle, Group, Image, Text } from "react-konva";
import virtualMachineImage from "../../../static/virtual_machine.svg";
import useImage from "use-image";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";

interface VirtualMachineEntityProps {
    entity: VirtualMachineEntityModel;
    onDragEnd: (x: number, y: number) => void;
}

const VirtualMachineEntity = ({ entity, onDragEnd }: VirtualMachineEntityProps) => {
    const { name, x, y } = entity;
    const [virtualMachineImageElement] = useImage(virtualMachineImage);

    return <Group
        x={x}
        y={y}
        onDragEnd={(e) => onDragEnd(e.target.x(), e.target.y())}
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