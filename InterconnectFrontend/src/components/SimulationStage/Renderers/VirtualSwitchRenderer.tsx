import { useEffect } from "react";
import { useVirtualSwitchEntitiesStore } from "../../../store/entitiesStore.ts";
import VirtualSwitchEntityContainer from "../Entity/VirtualSwitchEntityContainer.tsx";

const VirtualSwitchRenderer = () => {
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();

    useEffect(() => {
        (async () => {
            virtualSwitchEntitiesStore.fetchEntities();
        })();
    }, []);

    return virtualSwitchEntitiesStore.entities.map((e) => (
        <VirtualSwitchEntityContainer key={e.id} entity={e} />
    ));
};

export default VirtualSwitchRenderer;
