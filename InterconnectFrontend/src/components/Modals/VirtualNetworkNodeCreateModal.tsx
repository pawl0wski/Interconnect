import { Modal } from "@mantine/core";
import VirtualNetworkNodeCreateFormContainer from "../Forms/VirtualNetworkNodeCreateFormContainer.tsx";
import { useTranslation } from "react-i18next";

/**
 * Props for the `VirtualNetworkNodeCreateModal` component.
 */
interface VirtualNetworkNodeCreateModalProps {
    opened: boolean;
    onClose: () => void;
}

/**
 * Modal presenting the virtual network node creation form.
 * Closes after successful node creation via the form container.
 *
 * @param props Component props
 * @param props.opened Whether the modal is visible
 * @param props.onClose Handler invoked to close the modal
 * @returns A centered Mantine modal with the network node form
 */
const VirtualNetworkNodeCreateModal = ({
    opened,
    onClose,
}: VirtualNetworkNodeCreateModalProps) => {
    const { t } = useTranslation();

    return (
        <Modal
            title={t("virtualNetworkNode.form.newVirtualNetworkNode")}
            centered
            opened={opened}
            onClose={onClose}
        >
            <VirtualNetworkNodeCreateFormContainer onFormSubmitted={onClose} />
        </Modal>
    );
};

export default VirtualNetworkNodeCreateModal;
