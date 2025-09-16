import { EntityType } from "../models/enums/EntityType.ts";
import {
    useVirtualMachineEntitiesStore,
    useVirtualSwitchEntitiesStore,
} from "../store/entitiesStore.ts";
import { useTranslation } from "react-i18next";

const useEntityName = (id: number, type: EntityType) => {
    const virtualMachineEntitiesStore = useVirtualMachineEntitiesStore();
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();
    const { t } = useTranslation();

    switch (type) {
        case EntityType.VirtualMachine:
            const virtualMachine = virtualMachineEntitiesStore.getById(id);
            return virtualMachine?.name;
        case EntityType.VirtualSwitch:
            const virtualSwitch = virtualSwitchEntitiesStore.getById(id);
            return virtualSwitch?.name;
        case EntityType.Internet:
            return t("internet.internet");
        default:
            throw new Error("Unsupported EntityType");
    }
};

export default useEntityName;
