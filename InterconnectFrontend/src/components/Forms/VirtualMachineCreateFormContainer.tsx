import VirtualMachineCreateForm from "./VirtualMachineCreateForm.tsx";
import { LoadingOverlay } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useVirtualMachineCreateStore } from "../../store/virtualMachineCreateStore.ts";
import { useVirtualMachineEntitiesStore } from "../../store/entitiesStore.ts";
import { useErrorStore } from "../../store/errorStore.ts";
import VirtualMachineEntityType from "../../models/enums/VirtualMachineEntityType.ts";

/**
 * Props for the `VirtualMachineCreateFormContainer` component.
 */
interface VirtualMachineCreateFormContainerProps {
    onFormSubmitted: () => void;
}

/**
 * Form values used for VM creation.
 */
export interface VirtualMachineCreateFormValues {
    name: string;
    type: VirtualMachineEntityType;
    memory: number;
    virtualCPUs: number;
    bootableDiskId: string | null;
}

/**
 * Container component that manages validation and submission for creating a virtual machine.
 * It wires the form to the VM creation store, performs error handling, and refreshes the VM list.
 *
 * @param props Component props
 * @param props.onFormSubmitted Callback invoked after successful VM creation
 * @returns A loading overlay and the VM creation form
 */
const VirtualMachineCreateFormContainer = ({
    onFormSubmitted,
}: VirtualMachineCreateFormContainerProps) => {
    const virtualMachineCreateStore = useVirtualMachineCreateStore();
    const errorStore = useErrorStore();
    const virtualMachineEntityStore = useVirtualMachineEntitiesStore();
    const [isCreating, setIsCreating] = useState(false);
    const { t } = useTranslation();

    const form = useForm<VirtualMachineCreateFormValues>({
        mode: "uncontrolled",
        initialValues: {
            name: "",
            type: VirtualMachineEntityType.Host,
            memory: 0,
            virtualCPUs: 0,
            bootableDiskId: null,
        },
        validate: {
            name: (value) => {
                if (!value) {
                    return t("virtualMachine.form.nameIsEmptyValidationError");
                }
            },
            virtualCPUs: (value) => {
                if (!value) {
                    return t(
                        "virtualMachine.form.virtualCpuNotSetValidationError",
                    );
                }
            },
            memory: (value) => {
                if (!value) {
                    return t("virtualMachine.form.memoryNotSetValidationError");
                }
            },
            bootableDiskId: (value) => {
                if (!value) {
                    return t(
                        "virtualMachine.form.bootableDiskNotSelectedValidationError",
                    );
                }
            },
        },
    });

    const handleCreateVirtualMachine = async (
        values: VirtualMachineCreateFormValues,
    ) => {
        setIsCreating(true);
        try {
            virtualMachineCreateStore.update({
                ...values,
                bootableDiskId: parseInt(form.values.bootableDiskId!),
            });
            await virtualMachineCreateStore.createVirtualMachine();
            await virtualMachineEntityStore.fetchEntities();
            onFormSubmitted();
        } catch (error: any) {
            errorStore.setError(error, { showStackTrace: false });
        } finally {
            setIsCreating(false);
        }
    };

    return (
        <>
            <LoadingOverlay visible={isCreating} />
            <VirtualMachineCreateForm
                form={form}
                onFormSubmit={handleCreateVirtualMachine}
            />
        </>
    );
};

export default VirtualMachineCreateFormContainer;
