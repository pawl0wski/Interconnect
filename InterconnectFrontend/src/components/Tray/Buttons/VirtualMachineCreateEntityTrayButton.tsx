import { Button } from "@mantine/core";
import { useTranslation } from "react-i18next";

interface VirtualMachineCreateEntityTrayButtonProps {
    onClick: () => void;
}

const VirtualMachineCreateEntityTrayButton = ({ onClick }: VirtualMachineCreateEntityTrayButtonProps) => {
    const { t } = useTranslation();

    return <Button onClick={onClick}>{t("virtualMachine.addVirtualMachine")}</Button>;
};

export default VirtualMachineCreateEntityTrayButton;