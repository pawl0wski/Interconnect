import { create } from "zustand";

interface VirtualMachineCreateModalStore {
    opened: boolean;
    close: () => void;
    open: () => void;
}

const useVirtualMachineCreateModalStore = create<VirtualMachineCreateModalStore>((set) => ({
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

export { useVirtualMachineCreateModalStore };
