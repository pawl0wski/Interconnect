import { useMemo } from "react";
import useImage from "use-image";
import { KonvaEventObject } from "konva/lib/Node";
import { Circle, Group, Image, Text } from "react-konva";
import virtualMachineImageDefault from "../../../static/virtualMachine.svg";
import virtualMachineImageRunning from "../../../static/virtualMachineRunning.svg";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { VirtualMachineState } from "../../../models/enums/VirtualMachineState.ts";

interface VirtualMachineEntityProps {
    entity: VirtualMachineEntityModel;
    shapeName: string;
    draggable: boolean;
    onDragMove: (e: KonvaEventObject<DragEvent>) => void;
    onDragEnd: (e: KonvaEventObject<DragEvent>) => void;
    onMouseOver: (e: KonvaEventObject<MouseEvent>) => void;
    onMouseOut: (e: KonvaEventObject<MouseEvent>) => void;
    onClick: (e: KonvaEventObject<MouseEvent>) => void;
}

const VirtualMachineEntity = ({
                                  entity,
                                  shapeName,
                                  draggable,
                                  onDragEnd,
                                  onDragMove,
                                  onMouseOver,
                                  onMouseOut,
                                  onClick
                              }: VirtualMachineEntityProps) => {
    const { name, x, y } = entity;
    const [virtualMachineDefaultImageElement] = useImage(virtualMachineImageDefault);
    const [virtualMachineRunningImageElement] = useImage(virtualMachineImageRunning);

    const virtualMachineImage = useMemo(() => {
        switch (entity.state) {
            case VirtualMachineState.Booted:
                return virtualMachineRunningImageElement;
            default:
                return virtualMachineDefaultImageElement;
        }
    }, [entity.state, virtualMachineDefaultImageElement, virtualMachineRunningImageElement]);

    return <Group
        x={x}
        y={y}
        name={shapeName}
        onMouseOver={onMouseOver}
        onMouseOut={onMouseOut}
        onDragMove={onDragMove}
        onDragEnd={onDragEnd}
        onClick={onClick}
        draggable={draggable}
    >
        <Image
            height={50}
            width={50}
            image={virtualMachineImage}
        />
        <Circle />
        <Text y={60} fontStyle="bold" align="center" width={50} text={name} />
    </Group>;
};

export default VirtualMachineEntity;