import { Menu } from "@mantine/core";
import { MdOutlineCable, MdTerminal } from "react-icons/md";
import { PositionModel } from "../../../models/PositionModel.ts";
import { useTranslation } from "react-i18next";

interface VirtualMachineContextMenuProps {
    title: string;
    position: PositionModel;
    isVisible: boolean;

    onOpenVirtualMachineConsole: () => void;
    onStartPlacingVirtualNetwork: (slotId: number) => void;
}

const VirtualMachineContextMenu = ({
                                       title,
                                       position,
                                       isVisible,
                                       onOpenVirtualMachineConsole,
                                       onStartPlacingVirtualNetwork
                                   }: VirtualMachineContextMenuProps) => {
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
            <Menu.Item leftSection={<MdTerminal size={14} />} onClick={onOpenVirtualMachineConsole}>
                {t("terminal")}
            </Menu.Item>
            <Menu.Divider />
            <Menu.Label>
                {t("network")}
            </Menu.Label>
            {
                [...Array(4)].map((_, i) => (
                    <Menu.Item key={i} onClick={() => onStartPlacingVirtualNetwork(i + 1)}
                               leftSection={<MdOutlineCable size={14} />}>
                        {t("slot", { number: i + 1 })}
                    </Menu.Item>
                ))
            }
        </Menu.Dropdown>
    </Menu>;
};

export default VirtualMachineContextMenu;