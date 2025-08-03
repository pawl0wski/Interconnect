import { ReactNode, useEffect } from "react";
import { useErrorStore } from "./store/errorStore.ts";

interface ErrorBoundaryProps {
    children: ReactNode;
}

const ErrorBoundary = ({ children }: ErrorBoundaryProps) => {
    const errorStore = useErrorStore();


    useEffect(() => {
        const onError = (event: ErrorEvent) => {
            errorStore.setError(event.error);
        };

        const onUnhandledRejection = (event: PromiseRejectionEvent) => {
            errorStore.setError(event.reason);
        };

        window.addEventListener("error", onError);
        window.addEventListener("unhandledrejection", onUnhandledRejection);

        return () => {
            window.removeEventListener("error", onError);
            window.removeEventListener(
                "unhandledrejection",
                onUnhandledRejection
            );
        };
    }, [errorStore]);

    return <>
        {children}
    </>;
};

export default ErrorBoundary;