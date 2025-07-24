import { Button, Flex } from "@mantine/core";
import { MdCloudDone, MdCloudOff, MdSync } from "react-icons/md";
import { useMemo } from "react";
import "./ConnectionStatusIndicator.styles.scss";
import { ConnectionStatus } from "../../models/enums/ConnectionStatus.ts";

interface ConnectionStatusIndicatorProps {
    connectionStatus: ConnectionStatus;
    onClick: () => void;
}

const ConnectionStatusIndicator = ({ connectionStatus, onClick }: ConnectionStatusIndicatorProps) => {
    const componentClassName = "connection-status-indicator";

    const statusText = useMemo(() => {
        switch (connectionStatus) {
            case ConnectionStatus.Alive:
                return <span className={`${componentClassName}__text--is-alive`}>Połączono</span>;
            case ConnectionStatus.Dead:
                return <span className={`${componentClassName}__text--is-dead`}>Brak połączenia</span>;
            case ConnectionStatus.Unknown:
                return <span>Nieznane</span>;
        }
    }, [connectionStatus]);

    const statusIcon = useMemo(() => {
        switch (connectionStatus) {
            case ConnectionStatus.Alive:
                return <MdCloudDone
                    className={`${componentClassName}__icon ${componentClassName}__icon--is-alive`} />;
            case ConnectionStatus.Dead:
                return <MdCloudOff
                    className={`${componentClassName}__icon ${componentClassName}__icon--is-dead`} />;
            case ConnectionStatus.Unknown:
                return <MdSync
                    className={`${componentClassName}__icon`} />;
        }
    }, [connectionStatus]);

    return <Button onClick={onClick} variant="transparent" py="0">
        <Flex direction="row" align="center" gap={5}>
            {statusIcon}
            {statusText}
        </Flex>
    </Button>;
};

export default ConnectionStatusIndicator;