import { TextInput } from "@mantine/core";
import { useTranslation } from "react-i18next";
import { UseFormReturnType } from "@mantine/form";
import { VirtualMachineCreateFormValues } from "../VirtualMachineCreateFormContainer.tsx";

interface VirtualMachineNameInputProps {
    form: UseFormReturnType<VirtualMachineCreateFormValues>;
}

const VirtualMachineNameInput = ({ form }: VirtualMachineNameInputProps) => {
    const { t } = useTranslation();

    return <TextInput
        withAsterisk
        label={t("virtualMachine.name")}
        key={form.key("name")}
        {...form.getInputProps("name")}
    />;
};

export default VirtualMachineNameInput;