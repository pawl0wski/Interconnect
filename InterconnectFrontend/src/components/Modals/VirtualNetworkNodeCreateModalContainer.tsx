import { useVirtualNetworkNodeCreateModalStore } from "../../store/modals/modalStores.ts";
import VirtualNetworkNodeCreateModal from "./VirtualNetworkNodeCreateModal.tsx";

/**
 * Container that binds the virtual network node create modal to its store.
 * Passes `opened` and `close` to the presentational modal.
 * @returns The bound virtual network node create modal component
 */
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
