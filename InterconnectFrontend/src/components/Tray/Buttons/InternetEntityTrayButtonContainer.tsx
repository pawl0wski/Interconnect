import InternetEntityTrayButton from "./InternetEntityTrayButton.tsx";
import { useEntityPlacementStore } from "../../../store/entityPlacementStore.ts";
import { EntityType } from "../../../models/enums/EntityType.ts";
import { useMemo } from "react";

const InternetEntityTrayButtonContainer = () => {
    const entityPlacementStore = useEntityPlacementStore();

    const handleCreateInternet = () => {
        entityPlacementStore.setCurrentEntityType(EntityType.Internet);
    };

    const isActive = useMemo(() => (entityPlacementStore.currentEntityType == EntityType.Internet), [entityPlacementStore.currentEntityType]);


    return <InternetEntityTrayButton onClick={handleCreateInternet} active={isActive} />;
};

export default InternetEntityTrayButtonContainer;