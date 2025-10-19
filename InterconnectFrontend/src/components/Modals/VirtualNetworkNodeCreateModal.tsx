import { Modal } from "@mantine/core";
import VirtualNetworkNodeCreateFormContainer from "../Forms/VirtualNetworkNodeCreateFormContainer.tsx";

interface VirtualNetworkNodeCreateModalProps {
    opened: boolean;
    onClose: () => void;
}

const VirtualNetworkNodeCreateModal = ({
    opened,
    onClose,
}: VirtualNetworkNodeCreateModalProps) => {
    return (
        <Modal
            title="Nowy switch wirtualny"
            centered
            opened={opened}
            onClose={onClose}
        >
            <VirtualNetworkNodeCreateFormContainer onFormSubmitted={onClose} />
        </Modal>
    );
};

export default VirtualNetworkNodeCreateModal;
