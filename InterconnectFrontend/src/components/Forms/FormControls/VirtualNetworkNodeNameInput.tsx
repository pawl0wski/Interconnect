import { TextInput } from "@mantine/core";
import { UseFormReturnType } from "@mantine/form";
import { VirtualNetworkNodeCreateFormValues } from "../VirtualNetworkNodeCreateFormContainer.tsx";
import { useTranslation } from "react-i18next";

interface VirtualNetworkNodeNameInputProps {
    form: UseFormReturnType<VirtualNetworkNodeCreateFormValues>;
}

const VirtualNetworkNodeNameInput = ({ form }: VirtualNetworkNodeNameInputProps) => {
    const { t } = useTranslation();

    return (
        <TextInput
            withAsterisk
            label={t("virtualNetworkNode.form.name")}
            key={form.key("name")}
            {...form.getInputProps("name")}
        />
    );
};

export default VirtualNetworkNodeNameInput;
