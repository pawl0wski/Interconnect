import { Modal } from "@mantine/core";
import VirtualMachineCreateFormContainer from "../Forms/VirtualMachineCreateFormContainer.tsx";
import { useTranslation } from "react-i18next";

/**
 * Props for the `VirtualMachineCreateModal` component.
 */
interface VirtualMachineCreateModalProps {
    opened: boolean;

    onClose: () => void;
}

/**
 * Modal presenting the virtual machine creation form.
 * Controls visibility and closes upon successful creation via the form container.
 *
 * @param props Component props
 * @param props.opened Whether the modal is visible
 * @param props.onClose Handler invoked to close the modal
 * @returns A centered Mantine modal with the VM creation form
 */
const VirtualMachineCreateModal = ({
    opened,
    onClose,
}: VirtualMachineCreateModalProps) => {
    const { t } = useTranslation();

    return (
        <Modal
            title={t("virtualMachine.configuration")}
            opened={opened}
            onClose={onClose}
            centered
        >
            <VirtualMachineCreateFormContainer onFormSubmitted={onClose} />
        </Modal>
    );
};

export default VirtualMachineCreateModal;
