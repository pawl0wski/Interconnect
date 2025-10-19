import { Modal } from "@mantine/core";
import VirtualNetworkNodeCreateFormContainer from "../Forms/VirtualNetworkNodeCreateFormContainer.tsx";
import { useTranslation } from "react-i18next";

interface VirtualNetworkNodeCreateModalProps {
    opened: boolean;
    onClose: () => void;
}

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
