import { Layer, Stage } from "react-konva";
import classes from "./SimulationStage.module.scss";
import VirtualMachineEntityRenderer from "./Renderers/VirtualMachineEntityRenderer.tsx";
import classNames from "classnames";
import { KonvaEventObject } from "konva/lib/Node";
import { useMemo } from "react";
import SimulationContextMenuProvider from "./ContextMenu/SimulationContextMenuProvider.tsx";
import VirtualNetworkRenderer from "./Renderers/VirtualNetworkRenderer.tsx";
import VirtualNetworkNodeRenderer from "./Renderers/VirtualNetworkNodeRenderer.tsx";
import InternetEntityRenderer from "./Renderers/InternetEntityRenderer.tsx";

/**
 * Props for the `SimulationStage` component.
 */
interface SimulationStageProps {
    showPlacementCursor: boolean;
    onClick: (e: KonvaEventObject<MouseEvent>) => void;
    onContextMenu: (e: KonvaEventObject<PointerEvent>) => void;
}

/**
 * Main canvas for rendering the simulation: networks, machines, nodes, and the internet entity.
 * Applies a special cursor during placement mode and forwards stage events to handlers.
 *
 * @param props Component props
 * @param props.showPlacementCursor Whether to show special cursor when placing entities
 * @param props.onClick Click handler used for placements and selections
 * @param props.onContextMenu Context menu handler for per-entity actions
 * @returns A Konva stage with layered entity renderers
 */
const SimulationStage = ({
    showPlacementCursor,
    onClick,
    onContextMenu,
}: SimulationStageProps) => {
    const stageHeight = 2000;
    const stageWidth = 3000;

    const divClasses = useMemo(
        () =>
            classNames(
                classes["simulation-stage"],
                showPlacementCursor ? classes["is-placing"] : "",
            ),
        [showPlacementCursor],
    );

    return (
        <div className={divClasses}>
            <SimulationContextMenuProvider />
            <Stage
                width={stageWidth}
                height={stageHeight}
                onClick={onClick}
                onContextMenu={onContextMenu}
            >
                <Layer>
                    <VirtualNetworkRenderer />
                    <VirtualMachineEntityRenderer />
                    <VirtualNetworkNodeRenderer />
                    <InternetEntityRenderer />
                </Layer>
            </Stage>
        </div>
    );
};

export default SimulationStage;
