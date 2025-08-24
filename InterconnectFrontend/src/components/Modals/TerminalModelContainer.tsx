import TerminalModel from "./TerminalModal.tsx";
import { useCurrentVirtualMachineStore } from "../../store/currentVirtualMachineStore.ts";
import { useCurrentVirtualMachineModalStore } from "../../store/modals/currentVirtualMachineModalStore.ts";

const TerminalModelContainer = () => {
    const currentVirtualMachineStore = useCurrentVirtualMachineStore();
    const currentVirtualMachineModalStore = useCurrentVirtualMachineModalStore();

    const handleModalClose = () => {
        currentVirtualMachineModalStore.close();
    };

    return currentVirtualMachineStore.currentEntity &&
        <TerminalModel opened={currentVirtualMachineModalStore.opened}
                       onClose={handleModalClose}
                       entity={currentVirtualMachineStore.currentEntity!} />;
};

export default TerminalModelContainer;