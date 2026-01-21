import { Modal } from "@mantine/core";
import VirtualMachineTerminalView from "../VirtualMachineTerminalView.tsx";
import { VirtualMachineEntityModel } from "../../models/VirtualMachineEntityModel.ts";

/**
 * Props for the `TerminalModal` component.
 */
interface TerminalModelProps {
    entity: VirtualMachineEntityModel;
    opened: boolean;

    onClose: () => void;
}

/**
 * Modal that embeds an interactive terminal view for a VM.
 * Uses the VM's name as the title and disables escape-close to avoid session loss.
 *
 * @param props Component props
 * @param props.entity The VM entity whose console is displayed
 * @param props.opened Whether the modal is visible
 * @param props.onClose Handler invoked to close the modal
 * @returns A large Mantine modal with the VM terminal view
 */
const TerminalModal = ({ opened, entity, onClose }: TerminalModelProps) => {
    const { name, vmUuid } = entity;

    return (
        <Modal
            title={name}
            opened={opened}
            onClose={onClose}
            size="xl"
            closeOnEscape={false}
            centered
        >
            <VirtualMachineTerminalView uuid={vmUuid!} />
        </Modal>
    );
};

export default TerminalModal;
