import { Modal } from "@mantine/core";
import VirtualMachineCreateFormContainer from "../Forms/VirtualMachineCreateFormContainer.tsx";
import { useTranslation } from "react-i18next";

interface VirtualMachineCreateModalProps {
    opened: boolean;
    onClose: () => void;
}

const VirtualMachineCreateModal = ({ opened, onClose }: VirtualMachineCreateModalProps) => {
    const { t } = useTranslation();

    return <Modal
        title={t("virtualMachine.configuration")}
        opened={opened}
        onClose={onClose}
    >
        <VirtualMachineCreateFormContainer onFormSubmitted={onClose} />
    </Modal>;
};

export default VirtualMachineCreateModal;