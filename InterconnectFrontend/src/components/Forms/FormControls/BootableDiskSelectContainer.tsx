import BootableDiskSelect from "./BootableDiskSelect.tsx";
import { useBootableDisksStore } from "../../../store/bootableDisksStore.ts";
import { useEffect, useMemo } from "react";
import { Center, ComboboxItem, Loader } from "@mantine/core";

interface BootableDiskSelectContainerProps {
    selectedBootableDiskId: string | null;
    error: string | null;
    onBootableDiskChange: (bootableDisk: string | null) => void;
}

const BootableDiskSelectContainer = ({
    selectedBootableDiskId,
    error,
    onBootableDiskChange,
}: BootableDiskSelectContainerProps) => {
    const bootableDisksStore = useBootableDisksStore();

    useEffect(() => {
        bootableDisksStore.fetchBootableDisks();
    }, []);

    const bootableDiskComboboxItems = useMemo<ComboboxItem[]>(() => {
        return bootableDisksStore.bootableDisks.map(
            (bootableDisk) =>
                ({
                    label: `${bootableDisk.name} ${bootableDisk.version}`,
                    value: bootableDisk.id.toString(),
                }) as unknown as ComboboxItem,
        );
    }, [bootableDisksStore.bootableDisks]);

    return bootableDisksStore.isFetching ? (
        <Center>
            <Loader />
        </Center>
    ) : (
        <BootableDiskSelect
            withAsterisk
            selectedBootableDiskId={selectedBootableDiskId}
            bootableDiskItems={bootableDiskComboboxItems}
            error={error}
            onSelectedBootableDiskChange={(bootableDiskId) =>
                onBootableDiskChange(bootableDiskId)
            }
        />
    );
};

export default BootableDiskSelectContainer;
