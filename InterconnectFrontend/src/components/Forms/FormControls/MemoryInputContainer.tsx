import { useEffect, useState } from "react";
import { useConnectionInfoStore } from "../../../store/connectionInfoStore.ts";
import MemoryInput from "./MemoryInput.tsx";
import MemoryValueParser from "../../../utils/memoryValueParser.ts";
import { getConfiguration } from "../../../configuration.ts";

interface MemoryInputContainerProps {
    memory: number;
    error: string | null;
    onMemoryChange: (value: number) => void;
}

const MemoryInputContainer = ({ memory, error, onMemoryChange }: MemoryInputContainerProps) => {
    const defaultStringMemory = "0 B";
    const connectionInfoStore = useConnectionInfoStore();
    const [memoryLabel, setMemoryLabel] = useState(defaultStringMemory);
    const [memoryInput, setMemoryInput] = useState(defaultStringMemory);
    const config = getConfiguration();

    const maxMemory = connectionInfoStore.connectionInfo?.totalMemory ?? 0;
    const safeMaxMemory = maxMemory * config.maxSafeVirtualMachineMemoryPercent;

    const handleChangeMemoryUsingSlider = (memory: number) => {
        onMemoryChange(memory);
        const memoryHumanReadable = MemoryValueParser.valueMemoryToHumanReadable(memory);

        if (memoryHumanReadable) {
            setMemoryInput(memoryHumanReadable);
        }
    };

    const handleChangeMemoryUsingInput = (memory: string) => {
        setMemoryInput(memory);

        const parsedBytes = MemoryValueParser.humanReadableMemoryToValue(memory);
        if (parsedBytes) {
            onMemoryChange(parsedBytes);
        }
    };

    useEffect(() => {
        setMemoryLabel(MemoryValueParser.valueMemoryToHumanReadable(memory) ?? defaultStringMemory);
    }, [memory]);

    return <MemoryInput
        memorySlider={memory}
        memoryInput={memoryInput}
        memorySliderLabel={memoryLabel}
        maxTotalMemory={maxMemory}
        showSafeMemoryWarning={memory > safeMaxMemory}
        error={error}
        changeSelectedMemoryUsingSlider={handleChangeMemoryUsingSlider}
        changeSelectedMemoryUsingInput={handleChangeMemoryUsingInput}
    />;
};

export default MemoryInputContainer;