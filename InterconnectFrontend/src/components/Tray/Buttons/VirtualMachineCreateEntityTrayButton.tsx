import { useTranslation } from "react-i18next";
import { MdComputer } from "react-icons/md";
import TrayButton from "./TrayButton.tsx";

interface VirtualMachineCreateEntityTrayButtonProps {
    active: boolean;
    onClick: () => void;
}

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
