import { ComboboxItem } from "@mantine/core";
import VirtualMachineEntityType from "../../../models/enums/VirtualMachineEntityType.ts";
import VirtualMachineEntityTypeSelect from "./VirtualMachineEntityTypeSelect.tsx";
import { useTranslation } from "react-i18next";

interface VirtualMachineEntityTypeSelectContainerProps {
    selectedType: string;
    error: string | null;
    onChange: (type: string | null) => void;
}

const VirtualMachineEntityTypeSelectContainer = (
    props: VirtualMachineEntityTypeSelectContainerProps,
) => {
    const { t } = useTranslation();

    const typeComboboxItems: ComboboxItem[] = [
        {
            label: t("virtualMachineEntityType.host"),
            value: VirtualMachineEntityType.Host.toString(),
        },
        {
            label: t("virtualMachineEntityType.router"),
            value: VirtualMachineEntityType.Router.toString(),
        },
        {
            label: t("virtualMachineEntityType.server"),
            value: VirtualMachineEntityType.Server.toString(),
        },
    ];

    return (
        <VirtualMachineEntityTypeSelect
            withAsterisk
            typeItems={typeComboboxItems}
            {...props}
        />
    );
};

export default VirtualMachineEntityTypeSelectContainer;
