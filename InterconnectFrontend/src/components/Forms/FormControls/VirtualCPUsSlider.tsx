import { InputError, Slider, Text } from "@mantine/core";

interface VirtualCPUsSliderProps {
    maxVirtualCpusCount: number;
    cpuLabel: string;
    cpus: number;
    error: string | null;
    changeSelectedCpus: (cpus: number) => void;
}

const VirtualCPUsSlider = ({
    maxVirtualCpusCount,
    cpuLabel,
    cpus,
    error,
    changeSelectedCpus,
}: VirtualCPUsSliderProps) => {
    return (
        <div>
            <Text size="sm">CPU</Text>
            <Slider
                value={cpus}
                min={0}
                max={maxVirtualCpusCount}
                label={cpuLabel}
                onChange={changeSelectedCpus}
            />
            {error && <InputError>{error}</InputError>}
        </div>
    );
};

export default VirtualCPUsSlider;
