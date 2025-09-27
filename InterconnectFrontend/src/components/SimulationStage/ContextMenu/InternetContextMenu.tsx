import { Menu } from "@mantine/core";
import { MdOutlineCable } from "react-icons/md";
import NetworkConnectionMenuItemContainer from "./MenuItems/NetworkConnectionMenuItemContainer.tsx";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useTranslation } from "react-i18next";
import { PositionModel } from "../../../models/PositionModel.ts";
import VirtualNetworkConnectionModel from "../../../models/VirtualNetworkConnectionModel.ts";

interface InternetContextMenuProps {
    position: PositionModel;
    isVisible: boolean;
    entityId: number;
    connections: VirtualNetworkConnectionModel[];
    onStartPlacingVirtualNetwork: () => void;
}

const InternetContextMenu = ({
    position,
    isVisible,
    entityId,
    connections,
    onStartPlacingVirtualNetwork,
}: InternetContextMenuProps) => {
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
                <Menu.Label>{t("internet.internet")}</Menu.Label>
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
                        parentEntityId={entityId}
                        parentEntityType={EntityType.VirtualMachine}
                        connection={connection}
                    />
                ))}
            </Menu.Dropdown>
        </Menu>
    );
};

export default InternetContextMenu;
