import ErrorModal from "./ErrorModal.tsx";
import { useErrorStore } from "../../store/errorStore.ts";

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
