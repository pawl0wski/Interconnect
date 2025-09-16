import { Modal } from "@mantine/core";
import VirtualSwitchCreateFormContainer from "../Forms/VirtualSwitchCreateFormContainer.tsx";

interface VirtualSwitchCreateModalProps {
    opened: boolean;
    onClose: () => void;
}

const VirtualSwitchCreateModal = ({
    opened,
    onClose,
}: VirtualSwitchCreateModalProps) => {
    return (
        <Modal
            title="Nowy switch wirtualny"
            centered
            opened={opened}
            onClose={onClose}
        >
            <VirtualSwitchCreateFormContainer onFormSubmitted={onClose} />
        </Modal>
    );
};

export default VirtualSwitchCreateModal;
