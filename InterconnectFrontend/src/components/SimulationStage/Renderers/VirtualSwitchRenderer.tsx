import { useEffect } from "react";
import useVirtualSwitchEntitiesStore from "../../../store/virtualSwitchEntitiesStore.ts";
import VirtualSwitchEntityContainer from "../Entity/VirtualSwitchEntityContainer.tsx";

const VirtualSwitchRenderer = () => {
    const virtualSwitchEntitiesStore = useVirtualSwitchEntitiesStore();

    useEffect(() => {
        (async () => {
            virtualSwitchEntitiesStore.fetchEntities();
        })();
    }, []);

    return virtualSwitchEntitiesStore.virtualSwitchEntities.map((e) => (
        <VirtualSwitchEntityContainer key={e.id} entity={e} />
    ));
};

export default VirtualSwitchRenderer;