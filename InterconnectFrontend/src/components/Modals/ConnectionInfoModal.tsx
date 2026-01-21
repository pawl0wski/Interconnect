import ConnectionInfoModel from "../../models/ConnectionInfoModel.ts";
import { Center, Loader, Modal, Table } from "@mantine/core";

/**
 * Props for the `ConnectionInfoModal` component.
 */
interface ConnectionInfoModalProps {
    opened: boolean;
    connectionInfo: ConnectionInfoModel | null;
    closeModal: () => void;
}

/**
 * Modal that displays hypervisor connection information.
 * Shows a loader while data is unavailable, then renders a key-value table.
 *
 * @param props Component props
 * @param props.opened Whether the modal is visible
 * @param props.connectionInfo Connection info object; when null, a loader is shown
 * @param props.closeModal Handler invoked to close the modal
 * @returns A centered modal with connection details
 */
const ConnectionInfoModal = ({
    connectionInfo,
    opened,
    closeModal,
}: ConnectionInfoModalProps) => {
    return (
        <Modal
            opened={opened}
            onClose={closeModal}
            title="Informacje o połączeniu"
            centered
        >
            {!connectionInfo ? (
                <Center>
                    <Loader />
                </Center>
            ) : (
                <Table>
                    <Table.Thead>
                        <Table.Tr>
                            <Table.Th>Informacja</Table.Th>
                            <Table.Th>Wartość</Table.Th>
                        </Table.Tr>
                    </Table.Thead>
                    <Table.Tbody>
                        {Object.entries(connectionInfo).map(([key, value]) => (
                            <Table.Tr key={key}>
                                <Table.Td>{key}</Table.Td>
                                <Table.Td>{String(value)}</Table.Td>
                            </Table.Tr>
                        ))}
                    </Table.Tbody>
                </Table>
            )}
        </Modal>
    );
};

export default ConnectionInfoModal;
