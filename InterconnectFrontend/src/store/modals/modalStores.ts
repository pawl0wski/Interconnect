import { create } from "zustand/react";

/**
 * Generic modal store interface for controlling open/close state.
 */
interface ModalStore {
    /** Whether the modal is currently opened */
    opened: boolean;
    /** Closes the modal */
    close: () => void;
    /** Opens the modal */
    open: () => void;
}

/**
 * Factory function that creates a Zustand modal store with `open`/`close` helpers.
 * @returns A new modal store instance with `opened`, `open`, and `close`
 */
export const createModalStore = () => {
    return create<ModalStore>((set) => ({
        opened: false,
        close: () => set({ opened: false }),
        open: () => set({ opened: true }),
    }));
};

/** Modal controlling display of hypervisor connection info. */
export const useConnectionInfoModalStore = createModalStore();
/** Modal for displaying the currently selected virtual machine. */
export const useCurrentVirtualMachineModalStore = createModalStore();
/** Modal for creating a new virtual machine. */
export const useVirtualMachineCreateModalStore = createModalStore();
/** Modal for creating a new virtual network node. */
export const useVirtualNetworkNodeCreateModalStore = createModalStore();
/** Modal for inspecting packet details captured by the sniffer. */
export const usePacketDetailsModalStore = createModalStore();
