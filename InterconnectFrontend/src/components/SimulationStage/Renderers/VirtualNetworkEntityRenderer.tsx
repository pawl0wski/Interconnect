import useNetworkConnectionsStore from "../../../store/networkConnectionsStore.ts";
import { useEffect } from "react";
import VirtualNetworkConnectionContainer from "../Entity/VirtualNetworkConnectionContainer.tsx";

const VirtualNetworkEntityRenderer = () => {
    const virtualNetworkEntitiesStore = useNetworkConnectionsStore();

    useEffect(() => {
        (async () => {
            await virtualNetworkEntitiesStore.fetch();
        })();
    }, []);

    return virtualNetworkEntitiesStore.networkConnections.map(e => (
        <VirtualNetworkConnectionContainer key={e.id}
                                           virtualNetworkEntity={e} />
    ));
};

export default VirtualNetworkEntityRenderer;