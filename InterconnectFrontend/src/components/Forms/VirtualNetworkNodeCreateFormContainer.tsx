import { useForm } from "@mantine/form";
import VirtualNetworkNodeCreateForm from "./VirtualNetworkNodeCreateForm.tsx";
import { useCallback, useState } from "react";
import { LoadingOverlay } from "@mantine/core";
import useVirtualNetworkNodeCreateStore from "../../store/virtualNetworkNodeCreateStore.ts";
import { useVirtualNetworkNodeEntitiesStore } from "../../store/entitiesStore.ts";

/**
 * Props for the `VirtualNetworkNodeCreateFormContainer` component.
 */
interface VirtualNetworkNodeCreateFormContainerProps {
    onFormSubmitted: () => void;
}

/**
 * Form values used when creating a virtual network node.
 */
export interface VirtualNetworkNodeCreateFormValues {
    name: string;
}

/**
 * Container that manages the virtual network node create form lifecycle.
 * Handles validation, store updates, entity refresh, and loading state.
 *
 * @param props Component props
 * @param props.onFormSubmitted Callback invoked after successful node creation
 * @returns Loading overlay and the network node creation form
 */
const VirtualNetworkNodeCreateFormContainer = ({
    onFormSubmitted,
}: VirtualNetworkNodeCreateFormContainerProps) => {
    const [isCreating, setIsCreating] = useState(false);
    const virtualNetworkNodeCreateStore = useVirtualNetworkNodeCreateStore();
    const virtualNetworkNodeEntitiesStore = useVirtualNetworkNodeEntitiesStore();

    const form = useForm<VirtualNetworkNodeCreateFormValues>({
        mode: "uncontrolled",
        initialValues: {
            name: "",
        },
        validate: {
            name: (value) => {
                if (!value) {
                    return "Nazwa nie może być pusta";
                }
            },
        },
    });

    const handleFormSubmitted = useCallback(
        async (values: VirtualNetworkNodeCreateFormValues) => {
            setIsCreating(true);
            try {
                virtualNetworkNodeCreateStore.updateName(values.name);
                await virtualNetworkNodeCreateStore.create();
                await virtualNetworkNodeEntitiesStore.fetchEntities();
                onFormSubmitted();
            } finally {
                setIsCreating(false);
            }
        },
        [onFormSubmitted, virtualNetworkNodeCreateStore, virtualNetworkNodeEntitiesStore],
    );

    return (
        <>
            <LoadingOverlay visible={isCreating} />
            <VirtualNetworkNodeCreateForm
                form={form}
                onFormSubmit={handleFormSubmitted}
            />
        </>
    );
};

export default VirtualNetworkNodeCreateFormContainer;
