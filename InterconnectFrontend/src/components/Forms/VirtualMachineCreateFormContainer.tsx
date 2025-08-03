import { useForm } from "@mantine/form";
import VirtualMachineCreateForm from "./VirtualMachineCreateForm.tsx";
import { useTranslation } from "react-i18next";
import { useVirtualMachineCreateStore } from "../../store/virtualMachineCreateStore.ts";
import { useVirtualMachineEntitiesStore } from "../../store/virtualMachineEntitiesStore.ts";

interface VirtualMachineCreateFormContainerProps {
    onFormSubmitted: () => void;
}

export interface VirtualMachineCreateFormValues {
    name: string,
    memory: number,
    virtualCPUs: number,
    bootableDiskId: string | null,
}

const VirtualMachineCreateFormContainer = ({ onFormSubmitted }: VirtualMachineCreateFormContainerProps) => {
    const virtualMachineCreateStore = useVirtualMachineCreateStore();
    const virtualMachineEntityStore = useVirtualMachineEntitiesStore();
    const { t } = useTranslation();

    const form = useForm<VirtualMachineCreateFormValues>({
        mode: "uncontrolled",
        initialValues: {
            name: "",
            memory: 0,
            virtualCPUs: 0,
            bootableDiskId: null
        },
        validate: {
            name: (value) => {
                if (!value) {
                    return t("virtualMachine.form.nameIsEmptyValidationError");
                }
            },
            virtualCPUs: (value) => {
                if (!value) {
                    return t("virtualMachine.form.virtualCpuNotSetValidationError");
                }
            },
            memory: (value) => {
                if (!value) {
                    return t("virtualMachine.form.memoryNotSetValidationError");
                }
            },
            bootableDiskId: (value) => {
                if (!value) {
                    return t("virtualMachine.form.bootableDiskNotSelectedValidationError");
                }
            }
        }
    });

    const handleCreateVirtualMachine = async () => {
        virtualMachineCreateStore.update({ ...form.values, bootableDiskId: parseInt(form.values.bootableDiskId!) });
        await virtualMachineCreateStore.createVirtualMachine();
        await virtualMachineEntityStore.fetchEntities();
        onFormSubmitted();
    };

    return <VirtualMachineCreateForm form={form} onFormSubmit={handleCreateVirtualMachine} />;
};

export default VirtualMachineCreateFormContainer;