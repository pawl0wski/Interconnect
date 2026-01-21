import { UseFormReturnType } from "@mantine/form";
import { VirtualNetworkNodeCreateFormValues } from "./VirtualNetworkNodeCreateFormContainer.tsx";
import VirtualNetworkNodeNameInput from "./FormControls/VirtualNetworkNodeNameInput.tsx";
import { Button, Fieldset, Flex } from "@mantine/core";
import { useTranslation } from "react-i18next";

/**
 * Props for the `VirtualNetworkNodeCreateForm` component.
 */
interface VirtualNetworkNodeCreateFormProps {
    form: UseFormReturnType<VirtualNetworkNodeCreateFormValues>;
    onFormSubmit: (values: VirtualNetworkNodeCreateFormValues) => void;
}

/**
 * Simple form for creating a virtual network node (e.g., switch/router).
 * Renders a single name input and a submit button, leveraging Mantine form handling.
 *
 * @param props Component props
 * @param props.form Mantine form instance with network node form values
 * @param props.onFormSubmit Callback invoked on valid form submission
 * @returns JSX form for creating a virtual network node
 */
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
