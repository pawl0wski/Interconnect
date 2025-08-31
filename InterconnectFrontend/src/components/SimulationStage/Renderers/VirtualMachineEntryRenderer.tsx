import { useVirtualMachineEntitiesStore } from "../../../store/virtualMachineEntitiesStore.ts";
import VirtualMachineEntityContainer from "../Entity/VirtualMachineEntityContainer.tsx";
import { useEffect } from "react";

const VirtualMachineEntryRenderer = () => {
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();

    useEffect(() => {
        (async () => {
            await virtualMachineEntitiesStore.fetchEntities();
        })();
    }, []);

    return virtualMachineEntitiesStore.entities.map((e) => {
        return <VirtualMachineEntityContainer key={e.id} entity={e} />;
    });
};

export default VirtualMachineEntryRenderer;