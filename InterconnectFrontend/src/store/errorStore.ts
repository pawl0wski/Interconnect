import { create } from "zustand/react";
import StringUtils from "../utils/stringUtils.ts";

/**
 * State store for managing application errors and exception information.
 */
interface ErrorStore {
    /** The current error message, or null if no error */
    error: string | null;
    /** Stack trace of the error, or null if not captured */
    stackTrace: string | null;
    /**
     * Sets an error with optional stack trace capture.
     * @param {unknown} error The error object or message
     * @param {Object} args Optional configuration
     * @param {boolean} args.showStackTrace Whether to include the stack trace (default: true)
     */
    setError: (error: unknown, args?: { showStackTrace?: boolean }) => void;
    /** Clears the current error and stack trace */
    clearError: () => void;
}

/**
 * Zustand store hook for managing application errors globally.
 */
const useErrorStore = create<ErrorStore>()((set) => ({
    error: null,
    stackTrace: null,
    setError: (error: unknown, { showStackTrace = true } = {}) => {
        if (error instanceof Error) {
            set({
                error: StringUtils.capitalizeString(error.message),
                stackTrace: showStackTrace ? error.stack : null,
            });
        }
    },
    clearError: () => {
        set({ error: null, stackTrace: null });
    },
}));

export { useErrorStore };
