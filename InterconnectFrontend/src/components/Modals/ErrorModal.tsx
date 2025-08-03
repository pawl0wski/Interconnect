import { Modal, Textarea } from "@mantine/core";
import { useTranslation } from "react-i18next";

interface ErrorModalProps {
    error: string;
    stackTrace: string;
    opened: boolean;
    onModalClose: () => void;
}

const ErrorModal = ({ error, stackTrace, opened, onModalClose }: ErrorModalProps) => {
    const { t } = useTranslation();

    return <Modal title={t("errorOccurred")} opened={opened} onClose={onModalClose}>
        <p>{error}</p>
        <Textarea readOnly autosize maxRows={10} value={stackTrace} />
    </Modal>;
};

export default ErrorModal;