import { create } from "zustand/react";

interface ModalStore {
    opened: boolean;
    close: () => void;
    open: () => void;
}

export const createModalStore = () => {
    return create<ModalStore>((set) => ({
        opened: false,
        close: () => set({ opened: false }),
        open: () => set({ opened: true })
    }));
};

export const useConnectionInfoModalStore = createModalStore();
export const useCurrentVirtualMachineModalStore = createModalStore();
export const useVirtualMachineCreateModalStore = createModalStore();
export const useVirtualSwitchCreateModalStore = createModalStore();