import { Flex, Modal, Text, Textarea } from "@mantine/core";
import { useTranslation } from "react-i18next";
import { MdErrorOutline } from "react-icons/md";

/**
 * Props for the `ErrorModal` component.
 */
interface ErrorModalProps {
    error: string;
    stackTrace: string;
    opened: boolean;
    onModalClose: () => void;
}

/**
 * Title content for the error modal, with icon and translated label.
 * @returns A flex row with error icon and text
 */
const ErrorModalTitle = () => {
    const { t } = useTranslation();

    return (
        <Flex direction="row" justify="center" align="center" gap="0.5rem">
            <MdErrorOutline size="24" />
            <Text size="md">{t("errorOccurred")}</Text>
        </Flex>
    );
};

/**
 * Modal that displays an error message and optional stack trace.
 * @param props Component props
 * @param props.error The error message to display
 * @param props.stackTrace Optional stack trace to show in a textarea
 * @param props.opened Whether the modal is visible
 * @param props.onModalClose Handler invoked to close the modal
 * @returns A centered Mantine modal with error details
 */
const ErrorModal = ({
    error,
    stackTrace,
    opened,
    onModalClose,
}: ErrorModalProps) => {
    return (
        <Modal
            zIndex={400}
            title={<ErrorModalTitle />}
            opened={opened}
            onClose={onModalClose}
            centered
        >
            <p>{error}</p>
            {stackTrace && (
                <Textarea readOnly autosize maxRows={10} value={stackTrace} />
            )}
        </Modal>
    );
};

export default ErrorModal;
