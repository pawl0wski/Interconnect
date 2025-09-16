import VirtualNetworkConnectionModel from "../../../../models/VirtualNetworkConnectionModel.ts";
import NetworkConnectionMenuItem from "./NetworkConnectionMenuItem.tsx";
import { useCallback, useMemo } from "react";
import useEntityName from "../../../../hooks/useEntityName.ts";
import useNetworkConnectionsStore from "../../../../store/networkConnectionsStore.ts";
import useSimulationStageContextMenuClose from "../../../../hooks/useSimulationStageContextMenuClose.ts";
import { EntityType } from "../../../../models/enums/EntityType.ts";
import useFullscreenLoader from "../../../../hooks/useFullscreenLoader.ts";

interface NetworkConnectionMenuItemContainerProps {
    parentEntityId: number;
    parentEntityType: EntityType;
    connection: VirtualNetworkConnectionModel;
}

const NetworkConnectionMenuItemContainer = ({
    parentEntityId,
    parentEntityType,
    connection,
}: NetworkConnectionMenuItemContainerProps) => {
    const networkConnectionsStore = useNetworkConnectionsStore();
    const { closeContextMenu } = useSimulationStageContextMenuClose();
    const { startLoading, stopLoading } = useFullscreenLoader();

    const targetId = useMemo(
        () =>
            connection.sourceEntityId === parentEntityId &&
            connection.sourceEntityType === parentEntityType
                ? connection.destinationEntityId
                : connection.sourceEntityId,
        [
            connection.destinationEntityId,
            connection.sourceEntityId,
            connection.sourceEntityType,
            parentEntityId,
            parentEntityType,
        ],
    );
    const targetType = useMemo(
        () =>
            connection.sourceEntityId === parentEntityId &&
            connection.sourceEntityType === parentEntityType
                ? connection.destinationEntityType
                : connection.sourceEntityType,
        [
            connection.destinationEntityType,
            connection.sourceEntityId,
            connection.sourceEntityType,
            parentEntityId,
            parentEntityType,
        ],
    );

    const entityName = useEntityName(targetId, targetType);

    const handleNetworkConnectionDisconnect = useCallback(async () => {
        startLoading();
        await networkConnectionsStore.disconnectConnection(connection.id);
        closeContextMenu();
        stopLoading();
    }, [
        closeContextMenu,
        connection.id,
        networkConnectionsStore,
        startLoading,
        stopLoading,
    ]);

    return (
        <NetworkConnectionMenuItem
            entityName={entityName ?? ""}
            onClick={handleNetworkConnectionDisconnect}
        />
    );
};

export default NetworkConnectionMenuItemContainer;
