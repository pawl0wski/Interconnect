import { useVirtualMachineEntitiesStore } from "../../store/virtualMachineEntitiesStore.ts";
import VirtualMachineCreateEntitiesTray from "./VirtualMachineCreateEntitiesTray.tsx";
import { useCallback } from "react";

const VirtualMachineCreateEntitiesTrayContainer = () => {
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();

    const handleCreateVirtualMachine = useCallback(async () => {
        await virtualMachineEntitiesStore.createNewEntity(
            "fss",
            1,
            54
        );
    }, [virtualMachineEntitiesStore]);

    return <VirtualMachineCreateEntitiesTray onClick={handleCreateVirtualMachine} />;
};

export default VirtualMachineCreateEntitiesTrayContainer;