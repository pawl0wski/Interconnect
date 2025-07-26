import { Circle, Group, Image, Text } from "react-konva";
import virtualMachineImage from "../../../static/virtual_machine.svg";
import useImage from "use-image";

interface VirtualMachineEntityProps {
    name: string;
}

const VirtualMachineEntity = ({ name }: VirtualMachineEntityProps) => {
    const [virtualMachineImageElement] = useImage(virtualMachineImage);
    return <Group
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