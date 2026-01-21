import { useTranslation } from "react-i18next";
import { MdComputer } from "react-icons/md";
import TrayButton from "./TrayButton.tsx";

/**
 * Props for the `VirtualMachineCreateEntityTrayButton` component.
 */
interface VirtualMachineCreateEntityTrayButtonProps {
    active: boolean;
    onClick: () => void;
}

/**
 * Tray button that activates virtual machine placement mode.
 * @param props Component props
 * @param props.active Whether VM placement is currently active
 * @param props.onClick Handler to trigger VM placement
 * @returns A tray button with a computer icon and label
 */
const VirtualMachineCreateEntityTrayButton = ({
    active,
    onClick,
}: VirtualMachineCreateEntityTrayButtonProps) => {
    const { t } = useTranslation();

    return (
        <TrayButton
            active={active}
            icon={<MdComputer size={30} />}
            text={t("virtualMachine.virtualMachine")}
            onClick={onClick}
        />
    );
};

export default VirtualMachineCreateEntityTrayButton;
