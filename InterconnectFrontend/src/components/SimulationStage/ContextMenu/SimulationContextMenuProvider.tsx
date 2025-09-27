import VirtualMachineContextMenuContainer from "./VirtualMachineContextMenuContainer.tsx";
import VirtualSwitchContextMenuContainer from "./VirtualSwitchContextMenuContainer.tsx";
import InternetContextMenuContainer from "./InternetContextMenuContainer.tsx";

const SimulationContextMenuProvider = () => (
    <>
        <VirtualMachineContextMenuContainer />
        <VirtualSwitchContextMenuContainer />
        <InternetContextMenuContainer />
    </>
);

export default SimulationContextMenuProvider;
