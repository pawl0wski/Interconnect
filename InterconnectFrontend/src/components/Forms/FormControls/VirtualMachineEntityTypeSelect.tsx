import { ComboboxItem, Select } from "@mantine/core";
import { useTranslation } from "react-i18next";

interface VirtualMachineEntityTypeSelectProps {
    withAsterisk: boolean;
    typeItems: ComboboxItem[];
    selectedType: string;
    error: string | null;
    onChange: (type: string | null) => void;
}

const VirtualMachineEntityTypeSelect = ({
    withAsterisk,
    typeItems,
    selectedType,
    error,
    onChange,
}: VirtualMachineEntityTypeSelectProps) => {
    const { t } = useTranslation();

    return (
        <Select
            withAsterisk={withAsterisk}
            allowDeselect={false}
            label={t("virtualMachine.type")}
            value={selectedType}
            data={typeItems}
            error={error}
            onChange={onChange}
        />
    );
};

export default VirtualMachineEntityTypeSelect;
