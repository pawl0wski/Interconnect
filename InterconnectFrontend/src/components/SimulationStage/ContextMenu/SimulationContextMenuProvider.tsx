import VirtualMachineContextMenuContainer from "./VirtualMachineContextMenuContainer.tsx";
import VirtualNetworkNodeContextMenuContainer from "./VirtualNetworkNodeContextMenuContainer.tsx";
import InternetContextMenuContainer from "./InternetContextMenuContainer.tsx";

const SimulationContextMenuProvider = () => (
    <>
        <VirtualMachineContextMenuContainer />
        <VirtualNetworkNodeContextMenuContainer />
        <InternetContextMenuContainer />
    </>
);

export default SimulationContextMenuProvider;
