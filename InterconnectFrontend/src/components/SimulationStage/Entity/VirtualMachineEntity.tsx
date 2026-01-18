import { useMemo } from "react";
import useImage from "use-image";
import { KonvaEventObject } from "konva/lib/Node";
import { Group, Image, Text } from "react-konva";
import hostImageDefault from "../../../static/host.svg";
import hostImageRunning from "../../../static/hostRunning.svg";
import routerImageDefault from "../../../static/router.svg";
import routerImageRunning from "../../../static/routerRunning.svg";
import serverImageDefault from "../../../static/server.svg";
import serverImageRunning from "../../../static/serverRunning.svg";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { VirtualMachineState } from "../../../models/enums/VirtualMachineState.ts";
import CommunicationRoleEntityDescription from "../CommunicationRoleEntityDescription.tsx";
import CommunicationRole from "../../../models/enums/CommunicationRole.ts";
import VirtualMachineEntityType from "../../../models/enums/VirtualMachineEntityType.ts";

interface VirtualMachineEntityProps {
    entity: VirtualMachineEntityModel;
    shapeName: string;
    draggable: boolean;
    communicationRole?: CommunicationRole;
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
    communicationRole,
    onDragEnd,
    onDragMove,
    onMouseOver,
    onMouseOut,
    onClick,
}: VirtualMachineEntityProps) => {
    const { name, x, y } = entity;
    const [hostDefaultImageElement] = useImage(hostImageDefault);
    const [hostRunningImageElement] = useImage(hostImageRunning);
    const [routerDefaultImageElement] = useImage(routerImageDefault);
    const [routerRunningImageElement] = useImage(routerImageRunning);
    const [serverDefaultImageElement] = useImage(serverImageDefault);
    const [serverRunningImageElement] = useImage(serverImageRunning);

    const virtualMachineImage = useMemo(() => {
        switch (entity.type) {
            case VirtualMachineEntityType.Host:
                return entity.state == VirtualMachineState.Booted
                    ? hostRunningImageElement
                    : hostDefaultImageElement;
            case VirtualMachineEntityType.Router:
                return entity.state == VirtualMachineState.Booted
                    ? routerRunningImageElement
                    : routerDefaultImageElement;
            case VirtualMachineEntityType.Server:
                return entity.state == VirtualMachineState.Booted
                    ? serverRunningImageElement
                    : serverDefaultImageElement;
        }
    }, [
        entity.state,
        entity.type,
        hostDefaultImageElement,
        hostRunningImageElement,
        routerDefaultImageElement,
        routerRunningImageElement,
        serverDefaultImageElement,
        serverRunningImageElement,
    ]);

    return (
        <Group
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
            <Image height={50} width={50} image={virtualMachineImage} />
            <Text
                y={60}
                fontStyle="bold"
                align="center"
                width={50}
                text={name}
            />
            <CommunicationRoleEntityDescription
                y={75}
                role={communicationRole}
            />
        </Group>
    );
};

export default VirtualMachineEntity;
