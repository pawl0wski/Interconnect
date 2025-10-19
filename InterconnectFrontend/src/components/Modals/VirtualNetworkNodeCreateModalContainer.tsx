import { useVirtualNetworkNodeCreateModalStore } from "../../store/modals/modalStores.ts";
import VirtualNetworkNodeCreateModal from "./VirtualNetworkNodeCreateModal.tsx";

const VirtualNetworkNodeCreateModalContainer = () => {
    const virtualNetworkNodeCreateModal = useVirtualNetworkNodeCreateModalStore();

    return (
        <VirtualNetworkNodeCreateModal
            opened={virtualNetworkNodeCreateModal.opened}
            onClose={virtualNetworkNodeCreateModal.close}
        />
    );
};

export default VirtualNetworkNodeCreateModalContainer;
