import { useConnectionStore } from "../../store/connectionStore.ts";
import ConnectionStatusIndicator from "./ConnectionStatusIndicator.tsx";
import { useConnectionInfoModalStore } from "../../store/modals/modalStores.ts";
import { useCallback } from "react";
import { ConnectionStatus } from "../../models/enums/ConnectionStatus.ts";

const ConnectionStatusIndicatorContainer = () => {
    const connectionStatus = useConnectionStore((s) => s.connectionStatus);
    const connectionInfoModal = useConnectionInfoModalStore();

    const handleClick = useCallback(() => {
        if (connectionStatus !== ConnectionStatus.Alive) {
            return;
        }
        connectionInfoModal.open();
    }, [connectionInfoModal, connectionStatus]);

    return (
        <ConnectionStatusIndicator
            connectionStatus={connectionStatus}
            onClick={handleClick}
        />
    );
};

export default ConnectionStatusIndicatorContainer;
