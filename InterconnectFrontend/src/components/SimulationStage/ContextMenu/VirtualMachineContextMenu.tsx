import { Menu } from "@mantine/core";
import { MdOutlineCable, MdTerminal } from "react-icons/md";
import { PositionModel } from "../../../models/PositionModel.ts";
import { useTranslation } from "react-i18next";

interface VirtualMachineContextMenuProps {
    title: string;
    position: PositionModel;
    isVisible: boolean;

    openVirtualMachineConsole: () => void;
}

const VirtualMachineContextMenu = ({
                                       title,
                                       position,
                                       isVisible,
                                       openVirtualMachineConsole
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
            <Menu.Item leftSection={<MdTerminal size={14} />} onClick={openVirtualMachineConsole}>
                {t("terminal")}
            </Menu.Item>
            <Menu.Divider />
            <Menu.Label>
                {t("network")}
            </Menu.Label>
            <Menu.Item leftSection={<MdOutlineCable size={14} />}>
                {t("slot", { number: 1 })}
            </Menu.Item>
            <Menu.Item leftSection={<MdOutlineCable size={14} />}>
                {t("slot", { number: 2 })}
            </Menu.Item>
            <Menu.Item leftSection={<MdOutlineCable size={14} />}>
                {t("slot", { number: 3 })}
            </Menu.Item>
            <Menu.Item leftSection={<MdOutlineCable size={14} />}>
                {t("slot", { number: 4 })}
            </Menu.Item>
        </Menu.Dropdown>
    </Menu>;
};

export default VirtualMachineContextMenu;