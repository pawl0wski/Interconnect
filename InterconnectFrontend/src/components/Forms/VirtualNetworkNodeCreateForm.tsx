import { UseFormReturnType } from "@mantine/form";
import { VirtualNetworkNodeCreateFormValues } from "./VirtualNetworkNodeCreateFormContainer.tsx";
import VirtualNetworkNodeNameInput from "./FormControls/VirtualNetworkNodeNameInput.tsx";
import { Button, Fieldset, Flex } from "@mantine/core";
import { useTranslation } from "react-i18next";

interface VirtualNetworkNodeCreateFormProps {
    form: UseFormReturnType<VirtualNetworkNodeCreateFormValues>;
    onFormSubmit: (values: VirtualNetworkNodeCreateFormValues) => void;
}

const VirtualNetworkNodeCreateForm = ({
    form,
    onFormSubmit,
}: VirtualNetworkNodeCreateFormProps) => {
    const { t } = useTranslation();

    return (
        <form onSubmit={form.onSubmit(onFormSubmit)}>
            <Fieldset>
                <VirtualNetworkNodeNameInput form={form} />
            </Fieldset>
            <Flex justify="end" mt={10}>
                <Button type="submit">
                    {t("virtualNetworkNode.form.createVirtualNetworkNode")}
                </Button>
            </Flex>
        </form>
    );
};

export default VirtualNetworkNodeCreateForm;
