import { Layer, Stage } from "react-konva";
import "./SimulationStage.component.scss";
import VirtualMachineEntity from "./Entity/VirtualMachineEntity.tsx";

const SimulationStage = () => {
    const stageHeight = 2000;
    const stageWidth = 3000;

    return <div className="simulation-stage">
        <Stage width={stageWidth} height={stageHeight}>
            <Layer>
                <VirtualMachineEntity name={"VM-1"} />
            </Layer>
        </Stage>
    </div>;
};

export default SimulationStage;