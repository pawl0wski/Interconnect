import { Menu } from "@mantine/core";
import { MdDelete, MdOutlineCable, MdTerminal } from "react-icons/md";
import { PositionModel } from "../../../models/PositionModel.ts";
import { useTranslation } from "react-i18next";
import VirtualNetworkConnectionModel from "../../../models/VirtualNetworkConnectionModel.ts";
import NetworkConnectionMenuItemContainer from "./MenuItems/NetworkConnectionMenuItemContainer.tsx";
import { EntityType } from "../../../models/enums/EntityType.ts";

interface VirtualMachineContextMenuProps {
    entityId: number;
    title: string;
    position: PositionModel;
    isVisible: boolean;
    connections: VirtualNetworkConnectionModel[];

    onOpenVirtualMachineConsole: () => void;
    onStartPlacingVirtualNetwork: () => void;
    onDeleteEntity: () => void;
}

const VirtualMachineContextMenu = ({
    entityId,
    title,
    position,
    isVisible,
    connections,
    onOpenVirtualMachineConsole,
    onStartPlacingVirtualNetwork,
    onDeleteEntity,
}: VirtualMachineContextMenuProps) => {
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
                    leftSection={<MdTerminal size={14} />}
                    onClick={onOpenVirtualMachineConsole}
                >
                    {t("terminal")}
                </Menu.Item>
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
                        parentEntityId={entityId}
                        parentEntityType={EntityType.VirtualMachine}
                        connection={connection}
                    />
                ))}
            </Menu.Dropdown>
        </Menu>
    );
};

export default VirtualMachineContextMenu;
