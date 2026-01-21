import useFullscreenLoaderStore from "../store/fullscreenLoaderStore.ts";
import { useCallback } from "react";

/**
 * Return type for useFullscreenLoader hook.
 */
interface UseFullscreenLoaderReturnType {
    /** Function to start the fullscreen loader */
    startLoading: () => void;
    /** Function to stop the fullscreen loader */
    stopLoading: () => void;
}

/**
 * Custom hook that provides methods to control the fullscreen loader state.
 * Used for displaying loading indicators during async operations.
 * @returns {UseFullscreenLoaderReturnType} Object with startLoading and stopLoading functions
 */
const useFullscreenLoader = (): UseFullscreenLoaderReturnType => {
    const fullscreenLoaderStore = useFullscreenLoaderStore();

    const startLoading = useCallback(() => {
        fullscreenLoaderStore.enable();
    }, [fullscreenLoaderStore]);

    const stopLoading = useCallback(() => {
        fullscreenLoaderStore.disable();
    }, [fullscreenLoaderStore]);

    return { startLoading, stopLoading };
};

export default useFullscreenLoader;
