import { PositionModel } from "../../../models/PositionModel.ts";
import { useTranslation } from "react-i18next";
import { Menu } from "@mantine/core";
import { MdOutlineCable } from "react-icons/md";

interface VirtualSwitchContextMenuProps {
    title: string;
    position: PositionModel;
    isVisible: boolean;

    onStartPlacingVirtualNetwork: () => void;
}

const VirtualSwitchContextMenu = ({
                                      title,
                                      position,
                                      isVisible,
                                      onStartPlacingVirtualNetwork
                                  }: VirtualSwitchContextMenuProps) => {
    const { t } = useTranslation();

    return <Menu opened={isVisible}>
        <Menu.Dropdown style={{
            position: "absolute",
            top: position?.y ?? 0,
            left: position?.x ?? 0
        }}>
            <Menu.Label>
                {title}
            </Menu.Label>
            <Menu.Label>
                {t("network")}
            </Menu.Label>
            <Menu.Item onClick={() => onStartPlacingVirtualNetwork()} leftSection={<MdOutlineCable size={14} />}>
                {t("connectWithAnotherEntity")}
            </Menu.Item>
        </Menu.Dropdown>
    </Menu>;
};

export default VirtualSwitchContextMenu;