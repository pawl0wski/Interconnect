import { Modal } from "@mantine/core";
import VirtualMachineTerminalView from "../VirtualMachineTerminalView.tsx";
import { VirtualMachineEntityModel } from "../../models/VirtualMachineEntityModel.ts";

interface TerminalModelProps {
    entity: VirtualMachineEntityModel;
    opened: boolean;

    onClose: () => void;
}

const TerminalModal = ({ opened, entity, onClose }: TerminalModelProps) => {
    const { name, vmUuid } = entity;

    return <Modal
        title={name}
        opened={opened}
        onClose={onClose}
        size="xl"
        closeOnEscape={false}
        centered
    >
        <VirtualMachineTerminalView uuid={vmUuid!} />
    </Modal>;
};

export default TerminalModal;