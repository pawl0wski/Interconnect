import { useEffect } from "react";
import { useVirtualNetworkNodeEntitiesStore } from "../../../store/entitiesStore.ts";
import VirtualNetworkNodeEntityContainer from "../Entity/VirtualNetworkNodeEntityContainer.tsx";

const VirtualNetworkNodeRenderer = () => {
    const virtualNetworkNodeEntitiesStore = useVirtualNetworkNodeEntitiesStore();

    useEffect(() => {
        (async () => {
            virtualNetworkNodeEntitiesStore.fetchEntities();
        })();
    }, []);

    return virtualNetworkNodeEntitiesStore.entities.map((e) => (
        <VirtualNetworkNodeEntityContainer key={e.id} entity={e} />
    ));
};

export default VirtualNetworkNodeRenderer;
