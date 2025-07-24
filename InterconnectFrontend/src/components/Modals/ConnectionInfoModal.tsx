import ConnectionInfo from "../../models/ConnectionInfo.ts";
import { Center, Loader, Modal, Table } from "@mantine/core";

interface ConnectionInfoModalProps {
    opened: boolean;
    connectionInfo: ConnectionInfo | null;
    closeModal: () => void;
}

const ConnectionInfoModal = ({ connectionInfo, opened, closeModal }: ConnectionInfoModalProps) => {
    return (
        <Modal opened={opened} onClose={closeModal} title="Informacje o połączeniu" centered>
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
