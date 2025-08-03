import VirtualMachineCreateEntityTrayButton from "./VirtualMachineCreateEntityTrayButton.tsx";
import { useVirtualMachineCreateModalStore } from "../../../store/modals/virtualMachineCreateModalStore.ts";

const VirtualMachineCreateEntityTrayButtonContainer = () => {
    const virtualMachineCreateModalStore = useVirtualMachineCreateModalStore();


    const handleCreateVirtualMachine = () => {
        virtualMachineCreateModalStore.open();
    };

    return <VirtualMachineCreateEntityTrayButton onClick={handleCreateVirtualMachine} />;
};

export default VirtualMachineCreateEntityTrayButtonContainer;