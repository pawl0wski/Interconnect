import { useForm } from "@mantine/form";
import VirtualSwitchCreateForm from "./VirtualSwitchCreateForm.tsx";
import { useCallback, useState } from "react";
import { LoadingOverlay } from "@mantine/core";
import useVirtualSwitchCreateStore from "../../store/virtualSwitchCreateStore.ts";
import useVirtualSwitchEntitiesStore from "../../store/virtualSwitchEntitiesStore.ts";

interface VirtualSwitchCreateFormContainerProps {
    onFormSubmitted: () => void;
}

export interface VirtualSwitchCreateFormValues {
    name: string;
}

const VirtualSwitchCreateFormContainer = ({ onFormSubmitted }: VirtualSwitchCreateFormContainerProps) => {
    const [isCreating, setIsCreating] = useState(false);
    const virtualSwitchCreateStore = useVirtualSwitchCreateStore();
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();

    const form = useForm<VirtualSwitchCreateFormValues>({
        mode: "uncontrolled",
        initialValues: {
            name: ""
        },
        validate: {
            name: (value) => {
                if (!value) {
                    return "Nazwa nie może być pusta";
                }
            }
        }
    });

    const handleFormSubmitted = useCallback(async (values: VirtualSwitchCreateFormValues) => {
        setIsCreating(true);
        try {
            virtualSwitchCreateStore.updateName(values.name);
            await virtualSwitchCreateStore.create();
            await virtualSwitchEntitiesStore.fetchEntities();
            onFormSubmitted();
        } finally {
            setIsCreating(false);
        }
    }, [onFormSubmitted, virtualSwitchCreateStore, virtualSwitchEntitiesStore]);

    return <>
        <LoadingOverlay visible={isCreating} />
        <VirtualSwitchCreateForm form={form} onFormSubmit={handleFormSubmitted} />
    </>;
};

export default VirtualSwitchCreateFormContainer;