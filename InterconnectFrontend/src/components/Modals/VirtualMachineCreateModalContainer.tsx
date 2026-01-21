import { useMemo } from "react";
import { useVirtualMachineCreateModalStore } from "../../store/modals/modalStores.ts";
import VirtualMachineCreateModal from "./VirtualMachineCreateModal.tsx";

/**
 * Container that binds the VM create modal to its modal store state.
 * Reads `opened` and wires the `close` action.
 * @returns The bound VM create modal component
 */
const VirtualMachineCreateModalContainer = () => {
    const virtualMachineCreateModalStore = useVirtualMachineCreateModalStore();

    const isModalOpen = useMemo(
        () => virtualMachineCreateModalStore.opened,
        [virtualMachineCreateModalStore.opened],
    );

    return (
        <VirtualMachineCreateModal
            opened={isModalOpen}
            onClose={virtualMachineCreateModalStore.close}
        />
    );
};

export default VirtualMachineCreateModalContainer;
