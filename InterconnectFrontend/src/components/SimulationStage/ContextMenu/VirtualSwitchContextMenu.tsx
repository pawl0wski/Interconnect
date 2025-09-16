import { PositionModel } from "../../../models/PositionModel.ts";
import { useTranslation } from "react-i18next";
import { Menu } from "@mantine/core";
import { MdOutlineCable } from "react-icons/md";
import VirtualNetworkConnectionModel from "../../../models/VirtualNetworkConnectionModel.ts";
import NetworkConnectionMenuItemContainer from "./MenuItems/NetworkConnectionMenuItemContainer.tsx";
import { EntityType } from "../../../models/enums/EntityType.ts";

interface VirtualSwitchContextMenuProps {
    entityId: number;
    title: string;
    position: PositionModel;
    isVisible: boolean;
    connections: VirtualNetworkConnectionModel[];

    onStartPlacingVirtualNetwork: () => void;
}

const VirtualSwitchContextMenu = ({
    entityId,
    title,
    position,
    isVisible,
    connections,
    onStartPlacingVirtualNetwork,
}: VirtualSwitchContextMenuProps) => {
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
                <Menu.Label>{t("network")}</Menu.Label>
                <Menu.Item
                    onClick={() => onStartPlacingVirtualNetwork()}
                    leftSection={<MdOutlineCable size={14} />}
                >
                    {t("connectWithAnotherEntity")}
                </Menu.Item>
                {connections.map((connection) => (
                    <NetworkConnectionMenuItemContainer
                        parentEntityId={entityId}
                        parentEntityType={EntityType.VirtualSwitch}
                        connection={connection}
                    />
                ))}
            </Menu.Dropdown>
        </Menu>
    );
};

export default VirtualSwitchContextMenu;
