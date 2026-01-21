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

/**
 * Props for the `VirtualMachineCreateForm` component.
 */
interface VirtualMachineCreateFormProps {
    form: UseFormReturnType<VirtualMachineCreateFormValues>;
    onFormSubmit: (values: VirtualMachineCreateFormValues) => void;
}

/**
 * Virtual machine creation form that collects VM name, type, CPU and memory resources,
 * and a bootable disk selection. Submits values using Mantine form handler.
 *
 * - Uses translated section legends and labels.
 * - Delegates individual inputs to specialized form control components.
 *
 * @param props Component props
 * @param props.form Mantine form instance with VM create values
 * @param props.onFormSubmit Callback invoked on valid form submission
 * @returns JSX form for creating a virtual machine
 */
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
