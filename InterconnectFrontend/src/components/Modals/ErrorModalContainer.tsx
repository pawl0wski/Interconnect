import ErrorModal from "./ErrorModal.tsx";
import { useErrorStore } from "../../store/errorStore.ts";

/**
 * Container that binds the error modal to the global error store.
 * Shows the modal when an error exists and clears it on close.
 * @returns The error modal when an error is present, otherwise modal with hidden state
 */
const ErrorModalContainer = () => {
    const errorStore = useErrorStore();

    return (
        <ErrorModal
            opened={Boolean(errorStore.error)}
            error={errorStore.error!}
            stackTrace={errorStore.stackTrace!}
            onModalClose={errorStore.clearError}
        ></ErrorModal>
    );
};

export default ErrorModalContainer;
