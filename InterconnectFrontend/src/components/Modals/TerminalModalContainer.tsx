import TerminalModel from "./TerminalModal.tsx";
import { useCurrentVirtualMachineStore } from "../../store/currentVirtualMachineStore.ts";
import { useCurrentVirtualMachineModalStore } from "../../store/modals/modalStores.ts";

/**
 * Container that shows the terminal modal when a current VM is selected.
 * Binds open/close state to the current VM modal store.
 * @returns The terminal modal when a VM is selected, otherwise null
 */
const TerminalModalContainer = () => {
    const currentVirtualMachineStore = useCurrentVirtualMachineStore();
    const currentVirtualMachineModalStore =
        useCurrentVirtualMachineModalStore();

    const handleModalClose = () => {
        currentVirtualMachineModalStore.close();
    };

    return (
        currentVirtualMachineStore.currentEntity && (
            <TerminalModel
                opened={currentVirtualMachineModalStore.opened}
                onClose={handleModalClose}
                entity={currentVirtualMachineStore.currentEntity!}
            />
        )
    );
};

export default TerminalModalContainer;
