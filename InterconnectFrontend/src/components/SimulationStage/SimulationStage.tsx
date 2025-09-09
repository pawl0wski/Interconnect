import { Layer, Stage } from "react-konva";
import classes from "./SimulationStage.module.scss";
import VirtualMachineEntityRenderer from "./Renderers/VirtualMachineEntityRenderer.tsx";
import classNames from "classnames";
import { KonvaEventObject } from "konva/lib/Node";
import { useMemo } from "react";
import SimulationContextMenuProvider from "./ContextMenu/SimulationContextMenuProvider.tsx";
import VirtualNetworkRenderer from "./Renderers/VirtualNetworkRenderer.tsx";
import VirtualSwitchRenderer from "./Renderers/VirtualSwitchRenderer.tsx";
import InternetEntityRenderer from "./Renderers/InternetEntityRenderer.tsx";

interface SimulationStageProps {
    showPlacementCursor: boolean;
    onClick: (e: KonvaEventObject<MouseEvent>) => void;
    onContextMenu: (e: KonvaEventObject<PointerEvent>) => void;
}

const SimulationStage = ({ showPlacementCursor, onClick, onContextMenu }: SimulationStageProps) => {
    const stageHeight = 2000;
    const stageWidth = 3000;

    const divClasses = useMemo(() => (classNames(classes["simulation-stage"], showPlacementCursor ? classes["is-placing"] : "")), [showPlacementCursor]);

    return <div className={divClasses}>
        <SimulationContextMenuProvider />
        <Stage width={stageWidth} height={stageHeight} onClick={onClick} onContextMenu={onContextMenu}>
            <Layer>
                <VirtualNetworkRenderer />
                <VirtualMachineEntityRenderer />
                <VirtualSwitchRenderer />
                <InternetEntityRenderer />
            </Layer>
        </Stage>
    </div>;
};

export default SimulationStage;