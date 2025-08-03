import { useMemo } from "react";
import { useVirtualMachineCreateModalStore } from "../../store/modals/virtualMachineCreateModalStore.ts";
import VirtualMachineCreateModal from "./VirtualMachineCreateModal.tsx";

const VirtualMachineCreateModalContainer = () => {
    const virtualMachineCreateModalStore = useVirtualMachineCreateModalStore();

    const isModalOpen = useMemo(() =>
            (virtualMachineCreateModalStore.opened),
        [virtualMachineCreateModalStore.opened]);

    return <VirtualMachineCreateModal
        opened={isModalOpen}
        onClose={virtualMachineCreateModalStore.close}
    />;
};

export default VirtualMachineCreateModalContainer;