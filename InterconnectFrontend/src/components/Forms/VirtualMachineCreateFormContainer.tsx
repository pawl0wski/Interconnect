import VirtualMachineCreateForm from "./VirtualMachineCreateForm.tsx";
import { LoadingOverlay } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useVirtualMachineCreateStore } from "../../store/virtualMachineCreateStore.ts";
import { useVirtualMachineEntitiesStore } from "../../store/virtualMachineEntitiesStore.ts";
import { useErrorStore } from "../../store/errorStore.ts";

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
    const errorStore = useErrorStore();
    const virtualMachineEntityStore = useVirtualMachineEntitiesStore();
    const [isCreating, setIsCreating] = useState(false);
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

    const handleCreateVirtualMachine = async (values: VirtualMachineCreateFormValues) => {
        setIsCreating(true);
        try {
            virtualMachineCreateStore.update({ ...values, bootableDiskId: parseInt(form.values.bootableDiskId!) });
            await virtualMachineCreateStore.createVirtualMachine();
            await virtualMachineEntityStore.fetchEntities();
            onFormSubmitted();
        } catch (error: any) {
            errorStore.setError(error, { showStackTrace: false });
        } finally {
            setIsCreating(false);
        }
    };

    return <>
        <LoadingOverlay visible={isCreating} />
        <VirtualMachineCreateForm form={form} onFormSubmit={handleCreateVirtualMachine} />
    </>;
};

export default VirtualMachineCreateFormContainer;