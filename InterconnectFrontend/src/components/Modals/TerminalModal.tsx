import { Modal } from "@mantine/core";
import VirtualMachineTerminalView from "../VirtualMachineTerminalView.tsx";

interface TerminalModelProps {
    opened: boolean;
    uuid: string;

    onClose: () => void;
}

const TerminalModal = ({ opened, uuid, onClose }: TerminalModelProps) => {
    return <Modal
        opened={opened}
        onClose={onClose}
        size="xl"
        closeOnEscape={false}
        centered
    >
        <VirtualMachineTerminalView uuid={uuid} />
    </Modal>;
};

export default TerminalModal;