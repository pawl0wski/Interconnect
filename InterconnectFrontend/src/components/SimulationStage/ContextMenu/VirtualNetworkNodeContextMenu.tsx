import { PositionModel } from "../../../models/PositionModel.ts";
import { useTranslation } from "react-i18next";
import { Menu } from "@mantine/core";
import { MdDelete, MdOutlineCable } from "react-icons/md";
import VirtualNetworkConnectionModel from "../../../models/VirtualNetworkConnectionModel.ts";
import NetworkConnectionMenuItemContainer from "./MenuItems/NetworkConnectionMenuItemContainer.tsx";
import { EntityType } from "../../../models/enums/EntityType.ts";

interface VirtualNetworkNodeContextMenuProps {
    entityId: number;
    title: string;
    position: PositionModel;
    isVisible: boolean;
    connections: VirtualNetworkConnectionModel[];

    onStartPlacingVirtualNetwork: () => void;
    onDeleteEntity: () => void;
}

const VirtualNetworkNodeContextMenu = ({
    entityId,
    title,
    position,
    isVisible,
    connections,
    onStartPlacingVirtualNetwork,
    onDeleteEntity,
}: VirtualNetworkNodeContextMenuProps) => {
    const { t } = useTranslation();

    return (
        <Menu opened={isVisible}>
            <Menu.Dropdown
                style={{
                    position: "absolute",
                    top: position?.y ?? 0,
                    left: position?.x ?? 0,
                }}
            >
                <Menu.Label>{title}</Menu.Label>
                <Menu.Item
                    leftSection={<MdDelete size={14} color="red" />}
                    onClick={onDeleteEntity}
                >
                    {t("delete")}
                </Menu.Item>
                <Menu.Divider />
                <Menu.Label>{t("network")}</Menu.Label>
                <Menu.Item
                    onClick={() => onStartPlacingVirtualNetwork()}
                    leftSection={<MdOutlineCable size={14} />}
                >
                    {t("connectWithAnotherEntity")}
                </Menu.Item>
                {connections.map((connection) => (
                    <NetworkConnectionMenuItemContainer
                        key={connection.id}
                        parentEntityId={entityId}
                        parentEntityType={EntityType.VirtualNetworkNode}
                        connection={connection}
                    />
                ))}
            </Menu.Dropdown>
        </Menu>
    );
};

export default VirtualNetworkNodeContextMenu;
