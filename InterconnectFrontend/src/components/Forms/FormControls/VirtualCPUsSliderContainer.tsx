import { useCallback, useEffect, useState } from "react";
import VirtualCPUsSlider from "./VirtualCPUsSlider.tsx";
import { useConnectionInfoStore } from "../../../store/connectionInfoStore.ts";

interface VirtualCPUsSliderProps {
    virtualCPUs: number;
    error: string | null;
    onVirtualCPUsChange: (virtualCpus: number) => void;
}

const VirtualCPUsSliderContainer = ({
    virtualCPUs,
    error,
    onVirtualCPUsChange,
}: VirtualCPUsSliderProps) => {
    const defaultVirtualCpusString = "0 CPU";
    const connectionInfoStore = useConnectionInfoStore();
    const [cpuLabel, setCpuLabel] = useState(defaultVirtualCpusString);

    const maxVirtualCPUs = connectionInfoStore.connectionInfo?.cpuCount ?? 0;

    const handleChangeCpu = useCallback(
        (virtualCpus: number) => {
            onVirtualCPUsChange(virtualCpus);
        },
        [onVirtualCPUsChange],
    );

    useEffect(() => {
        setCpuLabel(`${virtualCPUs} CPU`);
    }, [virtualCPUs]);

    return (
        <VirtualCPUsSlider
            cpus={virtualCPUs}
            changeSelectedCpus={handleChangeCpu}
            cpuLabel={cpuLabel}
            error={error}
            maxVirtualCpusCount={maxVirtualCPUs}
        />
    );
};

export default VirtualCPUsSliderContainer;
