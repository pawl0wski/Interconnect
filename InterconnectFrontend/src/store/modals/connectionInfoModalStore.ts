import { create } from "zustand";

interface ConnectionInfoModalStore {
    opened: boolean;
    close: () => void;
    open: () => void;
}

const useConnectionInfoModalStore = create<ConnectionInfoModalStore>((set) => ({
    opened: false,
    close: () => {
        set({
            opened: false
        });
    },
    open: () => {
        set({
            opened: true
        });
    }
}));

export { useConnectionInfoModalStore };
