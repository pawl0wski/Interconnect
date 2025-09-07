import { UseFormReturnType } from "@mantine/form";
import { VirtualSwitchCreateFormValues } from "./VirtualSwitchCreateFormContainer.tsx";
import VirtualSwitchNameInput from "./FormControls/VirtualSwitchNameInput.tsx";
import { Button, Fieldset, Flex } from "@mantine/core";
import { useTranslation } from "react-i18next";

interface VirtualSwitchCreateFormProps {
    form: UseFormReturnType<VirtualSwitchCreateFormValues>;
    onFormSubmit: (values: VirtualSwitchCreateFormValues) => void;
}

const VirtualSwitchCreateForm = ({ form, onFormSubmit }: VirtualSwitchCreateFormProps) => {
    const { t } = useTranslation();

    return <form onSubmit={form.onSubmit(onFormSubmit)}>
        <Fieldset>
            <VirtualSwitchNameInput form={form} />
        </Fieldset>
        <Flex justify="end" mt={10}>
            <Button type="submit">{t("virtualSwitch.form.createVirtualSwitch")}</Button>
        </Flex>
    </form>;
};

export default VirtualSwitchCreateForm;