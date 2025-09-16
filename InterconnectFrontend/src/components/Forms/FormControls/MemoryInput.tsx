import { Box, Flex, Input, InputError, Slider, Text } from "@mantine/core";
import InputWarning from "../../InputWarning.tsx";
import { useTranslation } from "react-i18next";

interface MemoryInputProps {
    memorySlider: number;
    memoryInput: string;
    memorySliderLabel: string;
    maxTotalMemory: number;
    showSafeMemoryWarning: boolean;
    error: string | null;
    changeSelectedMemoryUsingSlider: (memory: number) => void;
    changeSelectedMemoryUsingInput: (memory: string) => void;
}

const MemoryInput = ({
    memoryInput,
    memorySlider,
    memorySliderLabel,
    maxTotalMemory,
    showSafeMemoryWarning,
    error,
    changeSelectedMemoryUsingSlider,
    changeSelectedMemoryUsingInput,
}: MemoryInputProps) => {
    const { t } = useTranslation();

    return (
        <div>
            <Text size="sm">{t("virtualMachine.operationalMemory")}</Text>
            <Flex direction="row" align="center" gap={10}>
                <Box style={{ flex: "0 0 75%" }}>
                    <Slider
                        value={memorySlider}
                        min={0}
                        max={maxTotalMemory}
                        label={memorySliderLabel}
                        onChange={changeSelectedMemoryUsingSlider}
                    />
                </Box>
                <Box style={{ flex: "0 0 25%" }}>
                    <Input
                        value={memoryInput}
                        onChange={(e) =>
                            changeSelectedMemoryUsingInput(e.target.value)
                        }
                    />
                </Box>
            </Flex>
            {error && <InputError>{error}</InputError>}
            {showSafeMemoryWarning && (
                <InputWarning
                    text={t(
                        "virtualMachine.form.excessiveMemorySelectionWarning",
                    )}
                />
            )}
        </div>
    );
};

export default MemoryInput;
