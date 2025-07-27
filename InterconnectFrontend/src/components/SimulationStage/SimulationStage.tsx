import { Layer, Stage } from "react-konva";
import classes from "./SimulationStage.module.scss";
import VirtualMachineEntryRenderer from "./Renderers/VirtualMachineEntryRenderer.tsx";

const SimulationStage = () => {
    const stageHeight = 2000;
    const stageWidth = 3000;

    return <div className={classes["simulation-stage"]}>
        <Stage width={stageWidth} height={stageHeight}>
            <Layer>
                <VirtualMachineEntryRenderer />
            </Layer>
        </Stage>
    </div>;
};

export default SimulationStage;