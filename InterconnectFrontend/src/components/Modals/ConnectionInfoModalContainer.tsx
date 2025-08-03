import { useConnectionInfoStore } from "../../store/connectionInfoStore.ts";
import ConnectionInfoModal from "./ConnectionInfoModal.tsx";
import { useCallback } from "react";
import { useConnectionInfoModalStore } from "../../store/modals/connectionInfoModalStore.ts";

const ConnectionInfoModalContainer = () => {
    const connectionInfoStore = useConnectionInfoStore();
    const connectionInfoModalStore = useConnectionInfoModalStore();

    const onCloseModal = useCallback(() => {
        connectionInfoModalStore.close();
    }, [connectionInfoModalStore]);

    return <ConnectionInfoModal connectionInfo={connectionInfoStore.connectionInfo}
                                opened={connectionInfoModalStore.opened}
                                closeModal={onCloseModal} />;
};

export default ConnectionInfoModalContainer;