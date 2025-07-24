import { useConnectionStore } from "../../store/connectionStore.ts";
import ConnectionStatusIndicator from "./ConnectionStatusIndicator.tsx";
import { useConnectionInfoModalStore } from "../../store/modals/connectionInfoModalStore.ts";
import { useCallback } from "react";

const ConnectionStatusIndicatorContainer = () => {
    const connectionStatus = useConnectionStore((s) => s.connectionStatus);
    const connectionInfoModal = useConnectionInfoModalStore();

    const handleClick = useCallback(() => {
        connectionInfoModal.open();
    }, [connectionInfoModal]);

    return <ConnectionStatusIndicator connectionStatus={connectionStatus} onClick={handleClick} />;
};

export default ConnectionStatusIndicatorContainer;