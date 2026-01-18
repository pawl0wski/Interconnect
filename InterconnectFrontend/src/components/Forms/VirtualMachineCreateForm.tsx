import { VirtualMachineCreateFormValues } from "./VirtualMachineCreateFormContainer.tsx";
import { UseFormReturnType } from "@mantine/form";
import { Button, Fieldset, Flex } from "@mantine/core";
import BootableDiskSelectContainer from "./FormControls/BootableDiskSelectContainer.tsx";
import VirtualCPUsSliderContainer from "./FormControls/VirtualCPUsSliderContainer.tsx";
import MemoryInputContainer from "./FormControls/MemoryInputContainer.tsx";
import ObjectUtils from "../../utils/objectUtils.ts";
import { useTranslation } from "react-i18next";
import VirtualMachineNameInput from "./FormControls/VirtualMachineNameInput.tsx";
import VirtualMachineEntityTypeSelectContainer from "./FormControls/VirtualMachineEntityTypeSelectContainer.tsx";
import VirtualMachineEntityType from "../../models/enums/VirtualMachineEntityType.ts";

interface VirtualMachineCreateFormProps {
    form: UseFormReturnType<VirtualMachineCreateFormValues>;
    onFormSubmit: (values: VirtualMachineCreateFormValues) => void;
}

const VirtualMachineCreateForm = ({
    form,
    onFormSubmit,
}: VirtualMachineCreateFormProps) => {
    const { t } = useTranslation();

    return (
        <form onSubmit={form.onSubmit(onFormSubmit)}>
            <Fieldset legend={t("virtualMachine.form.informationSection")}>
                <Flex gap="md" direction="column">
                    <VirtualMachineNameInput form={form} />
                    <VirtualMachineEntityTypeSelectContainer
                        selectedType={form.values.type.toString()}
                        error={ObjectUtils.getValueOrNull<string>(
                            form.errors,
                            "type",
                        )}
                        onChange={(v) =>
                            form.setFieldValue(
                                "type",
                                parseInt(
                                    v ??
                                        VirtualMachineEntityType.Host.toString(),
                                ) as VirtualMachineEntityType,
                            )
                        }
                    />
                </Flex>
            </Fieldset>
            <Fieldset legend={t("virtualMachine.form.resourcesSection")}>
                <Flex gap="md" direction="column">
                    <VirtualCPUsSliderContainer
                        virtualCPUs={form.values.virtualCPUs}
                        error={ObjectUtils.getValueOrNull<string>(
                            form.errors,
                            "virtualCPUs",
                        )}
                        onVirtualCPUsChange={(v) =>
                            form.setFieldValue("virtualCPUs", v)
                        }
                    />
                    <MemoryInputContainer
                        memory={form.values.memory}
                        error={ObjectUtils.getValueOrNull<string>(
                            form.errors,
                            "memory",
                        )}
                        onMemoryChange={(v) => form.setFieldValue("memory", v)}
                    />
                    <BootableDiskSelectContainer
                        selectedBootableDiskId={form.values.bootableDiskId}
                        error={ObjectUtils.getValueOrNull<string>(
                            form.errors,
                            "bootableDiskId",
                        )}
                        onBootableDiskChange={(v) =>
                            form.setFieldValue("bootableDiskId", v)
                        }
                    />
                </Flex>
            </Fieldset>
            <Flex direction="row" justify="flex-end" mt={10}>
                <Button type="submit">
                    {t("virtualMachine.form.createVirtualMachine")}
                </Button>
            </Flex>
        </form>
    );
};

export default VirtualMachineCreateForm;
