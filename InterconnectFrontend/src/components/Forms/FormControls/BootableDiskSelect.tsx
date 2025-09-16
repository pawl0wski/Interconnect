import { ComboboxItem, Select } from "@mantine/core";
import { useTranslation } from "react-i18next";

interface BootableDiskSelectProps {
    withAsterisk: boolean;
    bootableDiskItems: ComboboxItem[];
    selectedBootableDiskId: string | null;
    error: string | null;
    onSelectedBootableDiskChange: (bootableDiskId: string | null) => void;
}

const BootableDiskSelect = ({
    withAsterisk,
    bootableDiskItems,
    selectedBootableDiskId,
    error,
    onSelectedBootableDiskChange,
}: BootableDiskSelectProps) => {
    const { t } = useTranslation();

    return (
        <div>
            <Select
                withAsterisk={withAsterisk}
                label={t("virtualMachine.bootableDisk")}
                value={selectedBootableDiskId}
                data={bootableDiskItems}
                error={error}
                onChange={(value) => onSelectedBootableDiskChange(value)}
            />
        </div>
    );
};

export default BootableDiskSelect;
