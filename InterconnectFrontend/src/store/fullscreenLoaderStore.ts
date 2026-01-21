import { create } from "zustand/react";

/**
 * State store for managing the fullscreen loading overlay display.
 */
interface FullscreenLoaderStore {
    /** Whether the fullscreen loader is currently visible */
    isLoading: boolean;
    /** Shows the fullscreen loader */
    enable: () => void;
    /** Hides the fullscreen loader */
    disable: () => void;
}

/**
 * Zustand store hook for managing fullscreen loader visibility.
 * Used to display loading indicators during long-running operations.
 */
const useFullscreenLoaderStore = create<FullscreenLoaderStore>((set) => ({
    isLoading: false,
    enable: () => set({ isLoading: true }),
    disable: () => set({ isLoading: false }),
}));

export default useFullscreenLoaderStore;
