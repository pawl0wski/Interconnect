import useFullscreenLoaderStore from "../store/fullscreenLoaderStore.ts";
import { useCallback } from "react";

interface UseFullscreenLoaderReturnType {
    startLoading: () => void;
    stopLoading: () => void;
}

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
