import { create } from "zustand";

interface CurrentVirtualMachineModalStore {
    opened: boolean;
    close: () => void;
    open: () => void;
}

const useCurrentVirtualMachineModalStore = create<CurrentVirtualMachineModalStore>((set) => ({
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

export { useCurrentVirtualMachineModalStore };
