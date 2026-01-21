import { useConnectionInfoStore } from "../../store/connectionInfoStore.ts";
import ConnectionInfoModal from "./ConnectionInfoModal.tsx";
import { useCallback } from "react";
import { useConnectionInfoModalStore } from "../../store/modals/modalStores.ts";

/**
 * Container that binds the connection info modal to its store and data source.
 * Passes current connection info and modal open/close state.
 * @returns The connection info modal component
 */
const ConnectionInfoModalContainer = () => {
    const connectionInfoStore = useConnectionInfoStore();
    const connectionInfoModalStore = useConnectionInfoModalStore();

    const onCloseModal = useCallback(() => {
        connectionInfoModalStore.close();
    }, [connectionInfoModalStore]);

    return (
        <ConnectionInfoModal
            connectionInfo={connectionInfoStore.connectionInfo}
            opened={connectionInfoModalStore.opened}
            closeModal={onCloseModal}
        />
    );
};

export default ConnectionInfoModalContainer;
