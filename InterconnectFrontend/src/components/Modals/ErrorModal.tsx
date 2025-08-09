import { Flex, Modal, Textarea, Text } from "@mantine/core";
import { useTranslation } from "react-i18next";
import { MdErrorOutline } from "react-icons/md";

interface ErrorModalProps {
    error: string;
    stackTrace: string;
    opened: boolean;
    onModalClose: () => void;
}

const ErrorModalTitle = () => {
    const { t } = useTranslation();

    return <Flex direction="row" justify="center" align="center" gap="0.5rem">
        <MdErrorOutline size="24" />
        <Text size="md">{t("errorOccurred")}</Text>
    </Flex>;
};

const ErrorModal = ({ error, stackTrace, opened, onModalClose }: ErrorModalProps) => {
    return <Modal title={<ErrorModalTitle />} opened={opened} onClose={onModalClose}>
        <p>{error}</p>
        {
            stackTrace && <Textarea readOnly autosize maxRows={10} value={stackTrace} />
        }
    </Modal>;
};

export default ErrorModal;