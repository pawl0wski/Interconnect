import { create } from "zustand/react";

interface ErrorStore {
    error: string | null;
    stackTrace: string | null;
    setError: (error: unknown) => void;
    clearError: () => void;
}

const useErrorStore = create<ErrorStore>()((set) => ({
    error: null,
    stackTrace: null,
    setError: (error: unknown) => {
        if (error instanceof Error) {
            set({
                error: error.message,
                stackTrace: error.stack
            });
        }
    },
    clearError: () => {
        set({ error: null, stackTrace: null });
    }
}));

export { useErrorStore };