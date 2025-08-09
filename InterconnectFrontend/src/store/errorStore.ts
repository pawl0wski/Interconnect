import { create } from "zustand/react";
import StringUtils from "../utils/stringUtils.ts";

interface ErrorStore {
    error: string | null;
    stackTrace: string | null;
    setError: (error: unknown, args?: { showStackTrace?: boolean } ) => void;
    clearError: () => void;
}

const useErrorStore = create<ErrorStore>()((set) => ({
    error: null,
    stackTrace: null,
    setError: (error: unknown, { showStackTrace = true } = {}) => {
        if (error instanceof Error) {
            set({
                error: StringUtils.capitalizeString(error.message),
                stackTrace: showStackTrace ? error.stack : null
            });
        }
    },
    clearError: () => {
        set({ error: null, stackTrace: null });
    }
}));

export { useErrorStore };