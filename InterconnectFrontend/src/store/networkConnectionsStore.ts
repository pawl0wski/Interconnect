import { create } from "zustand/react";
import VirtualNetworkConnectionModel from "../models/VirtualNetworkConnectionModel.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";

interface NetworkConnectionsStore {
    networkConnections: VirtualNetworkConnectionModel[];
    fetch: () => Promise<void>;
    disconnectConnection: (id: number) => Promise<void>;
    getConnectionsForEntity: (
        id: number,
        type: EntityType,
    ) => VirtualNetworkConnectionModel[];
}

const useNetworkConnectionsStore = create<NetworkConnectionsStore>()((
    set,
    get,
) => {
    return {
        networkConnections: [],
        fetch: async () => {
            const response =
                await virtualNetworkResourceClient.getAllConnections();

            set({
                networkConnections: response.data,
            });
        },
        disconnectConnection: async (id: number) => {
            await virtualNetworkResourceClient.disconnectEntities({ id });
            set({
                networkConnections: get().networkConnections.filter(
                    (c) => c.id !== id,
                ),
            });
        },
        getConnectionsForEntity: (id: number, type: EntityType) =>
            get().networkConnections.filter(
                (c) =>
                    (c.sourceEntityId === id && c.sourceEntityType === type) ||
                    (c.destinationEntityId === id &&
                        c.destinationEntityType === type),
            ),
    };
});

export default useNetworkConnectionsStore;
