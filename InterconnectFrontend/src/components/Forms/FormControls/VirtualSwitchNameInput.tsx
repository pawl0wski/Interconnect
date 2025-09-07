import { TextInput } from "@mantine/core";
import { UseFormReturnType } from "@mantine/form";
import { VirtualSwitchCreateFormValues } from "../VirtualSwitchCreateFormContainer.tsx";
import { useTranslation } from "react-i18next";

interface VirtualSwitchNameInputProps {
    form: UseFormReturnType<VirtualSwitchCreateFormValues>;
}

const VirtualSwitchNameInput = ({ form }: VirtualSwitchNameInputProps) => {
    const { t } = useTranslation();

    return <TextInput
        withAsterisk
        label={t("virtualSwitch.form.name")}
        key={form.key("name")}
        {...form.getInputProps("name")}
    />;
};

export default VirtualSwitchNameInput;