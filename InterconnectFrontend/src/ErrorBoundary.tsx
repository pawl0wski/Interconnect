import { ReactNode, useEffect } from "react";
import { useErrorStore } from "./store/errorStore.ts";

interface ErrorBoundaryProps {
    children: ReactNode;
}

/**
 * Error boundary component that catches unhandled errors and promise rejections
 * from the application and stores them in the error store for display.
 * @param {ErrorBoundaryProps} props - Component props
 * @param {ReactNode} props.children - Child components to wrap
 * @returns {JSX.Element} The children wrapped in error handling
 */
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
                onUnhandledRejection,
            );
        };
    }, [errorStore]);

    return <>{children}</>;
};

export default ErrorBoundary;
