import { useVirtualSwitchCreateModalStore } from "../../store/modals/modalStores.ts";
import VirtualSwitchCreateModal from "./VirtualSwitchCreateModal.tsx";

const VirtualSwitchCreateModalContainer = () => {
    const virtualSwitchCreateModal = useVirtualSwitchCreateModalStore();

    return <VirtualSwitchCreateModal opened={virtualSwitchCreateModal.opened}
                                     onClose={virtualSwitchCreateModal.close} />;
};

export default VirtualSwitchCreateModalContainer;