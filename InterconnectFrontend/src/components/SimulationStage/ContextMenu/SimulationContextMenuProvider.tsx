import VirtualMachineContextMenuContainer from "./VirtualMachineContextMenuContainer.tsx";
import VirtualSwitchContextMenuContainer from "./VirtualSwitchContextMenuContainer.tsx";

const SimulationContextMenuProvider = () => (
    <>
        <VirtualMachineContextMenuContainer />
        <VirtualSwitchContextMenuContainer />
    </>
);

export default SimulationContextMenuProvider;
