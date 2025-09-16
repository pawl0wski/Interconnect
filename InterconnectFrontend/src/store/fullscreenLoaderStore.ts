import { create } from "zustand/react";

interface FullscreenLoaderStore {
    isLoading: boolean;
    enable: () => void;
    disable: () => void;
}

const useFullscreenLoaderStore = create<FullscreenLoaderStore>((set) => ({
    isLoading: false,
    enable: () => set({ isLoading: true }),
    disable: () => set({ isLoading: false }),
}));

export default useFullscreenLoaderStore;
