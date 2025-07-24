import { useConnectionInfoStore } from "../../store/connectionInfoStore.ts";
import ConnectionInfoModal from "./ConnectionInfoModal.tsx";
import { useCallback, useEffect } from "react";
import { useConnectionInfoModalStore } from "../../store/modals/connectionInfoModalStore.ts";

const ConnectionInfoModalContainer = () => {
    const connectionInfoStore = useConnectionInfoStore();
    const connectionInfoModalStore = useConnectionInfoModalStore();

    const onCloseModal = useCallback(() => {
        connectionInfoStore.clearConnectionInfo();
        connectionInfoModalStore.close();
    }, [connectionInfoModalStore, connectionInfoStore]);

    useEffect(() => {
        if (connectionInfoModalStore.opened &&
            !connectionInfoStore.connectionInfo) {
            connectionInfoStore.updateConnectionInfo();
        }
    }, [connectionInfoModalStore.opened, connectionInfoStore]);

    return <ConnectionInfoModal connectionInfo={connectionInfoStore.connectionInfo}
                                opened={connectionInfoModalStore.opened}
                                closeModal={onCloseModal} />;
};

export default ConnectionInfoModalContainer;