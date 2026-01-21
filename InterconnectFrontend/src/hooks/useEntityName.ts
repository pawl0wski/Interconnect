import { EntityType } from "../models/enums/EntityType.ts";
import {
    useVirtualMachineEntitiesStore,
    useVirtualNetworkNodeEntitiesStore,
} from "../store/entitiesStore.ts";
import { useTranslation } from "react-i18next";

/**
 * Custom hook that retrieves the name of an entity by its ID and type.
 * Handles translation for special entities like Internet.
 * @param {number} id The ID of the entity
 * @param {EntityType} type The type of the entity
 * @returns {string | undefined} The entity name, or undefined if not found
 */
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
