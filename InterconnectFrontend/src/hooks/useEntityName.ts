import { EntityType } from "../models/enums/EntityType.ts";
import {
    useVirtualMachineEntitiesStore,
    useVirtualNetworkNodeEntitiesStore,
} from "../store/entitiesStore.ts";
import { useTranslation } from "react-i18next";

const useEntityName = (id: number, type: EntityType) => {
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();
    const virtualNetworkNodeEntitiesStore = useVirtualNetworkNodeEntitiesStore();
    const { t } = useTranslation();

    switch (type) {
        case EntityType.VirtualMachine:
            const virtualMachine = virtualMachineEntitiesStore.getById(id);
            return virtualMachine?.name;
        case EntityType.VirtualNetworkNode:
            const virtualNetworkNode = virtualNetworkNodeEntitiesStore.getById(id);
            return virtualNetworkNode?.name;
        case EntityType.Internet:
            return t("internet.internet");
        default:
            throw new Error("Unsupported EntityType");
    }
};

export default useEntityName;
