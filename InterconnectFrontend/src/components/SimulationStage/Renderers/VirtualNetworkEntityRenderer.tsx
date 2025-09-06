import useVirtualNetworkEntitiesStore from "../../../store/virtualNetworkEntitiesStore.ts";
import { useEffect } from "react";
import VirtualNetworkEntityContainer from "../Entity/VirtualNetworkEntityContainer.tsx";

const VirtualNetworkEntityRenderer = () => {
    const virtualNetworkEntitiesStore = useVirtualNetworkEntitiesStore();

    useEffect(() => {
        (async () => {
            await virtualNetworkEntitiesStore.fetchVirtualNetworkEntities();
        })();
    }, []);

    return virtualNetworkEntitiesStore.virtualNetworkEntities.map(e => (
        <VirtualNetworkEntityContainer key={e.firstEntityUuid}
                                       virtualNetworkEntity={e} />
    ));
};

export default VirtualNetworkEntityRenderer;