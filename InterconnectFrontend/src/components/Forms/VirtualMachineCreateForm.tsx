import { VirtualMachineCreateFormValues } from "./VirtualMachineCreateFormContainer.tsx";
import { UseFormReturnType } from "@mantine/form";
import { Button, Fieldset, Flex } from "@mantine/core";
import BootableDiskSelectContainer from "./FormControls/BootableDiskSelectContainer.tsx";
import VirtualCPUsSliderContainer from "./FormControls/VirtualCPUsSliderContainer.tsx";
import MemoryInputContainer from "./FormControls/MemoryInputContainer.tsx";
import ObjectUtils from "../../utils/objectUtils.ts";
import { useTranslation } from "react-i18next";
import VirtualMachineNameInput from "./FormControls/VirtualMachineNameInput.tsx";

interface VirtualMachineCreateFormProps {
    form: UseFormReturnType<VirtualMachineCreateFormValues>;
    onFormSubmit: (values: VirtualMachineCreateFormValues) => void;
}

const VirtualMachineCreateForm = ({ form, onFormSubmit }: VirtualMachineCreateFormProps) => {
    const { t } = useTranslation();

    return <form onSubmit={form.onSubmit(onFormSubmit)}>
        <Fieldset legend={t("virtualMachine.form.informationSection")}>
            <VirtualMachineNameInput form={form} />
        </Fieldset>
        <Fieldset legend={t("virtualMachine.form.resourcesSection")}>
            <Flex gap="md" direction="column">
                <VirtualCPUsSliderContainer
                    virtualCPUs={form.values.virtualCPUs}
                    error={ObjectUtils.getValueOrNull<string>(form.errors, "virtualCPUs")}
                    onVirtualCPUsChange={(v) => form.setFieldValue("virtualCPUs", v)}
                />
                <MemoryInputContainer
                    memory={form.values.memory}
                    error={ObjectUtils.getValueOrNull<string>(form.errors, "memory")}
                    onMemoryChange={(v) => form.setFieldValue("memory", v)}
                />
                <BootableDiskSelectContainer
                    selectedBootableDiskId={form.values.bootableDiskId}
                    error={ObjectUtils.getValueOrNull<string>(form.errors, "bootableDiskId")}
                    onBootableDiskChange={(v) => form.setFieldValue("bootableDiskId", v)}
                />
            </Flex>
        </Fieldset>
        <Flex direction="row" justify="flex-end" mt={10}>
            <Button type="submit">{t("virtualMachine.form.createVirtualMachine")}</Button>
        </Flex>
    </form>;
};

export default VirtualMachineCreateForm;