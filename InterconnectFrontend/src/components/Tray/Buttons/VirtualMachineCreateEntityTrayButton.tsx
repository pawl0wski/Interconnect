import { useTranslation } from "react-i18next";
import { MdComputer } from "react-icons/md";
import TrayButton from "./TrayButton.tsx";

interface VirtualMachineCreateEntityTrayButtonProps {
    onClick: () => void;
}

const VirtualMachineCreateEntityTrayButton = ({ onClick }: VirtualMachineCreateEntityTrayButtonProps) => {
    const { t } = useTranslation();

    return <TrayButton
        icon={<MdComputer size={30} />}
        text={t("virtualMachine.virtualMachine")}
        onClick={onClick} />;
};

export default VirtualMachineCreateEntityTrayButton;