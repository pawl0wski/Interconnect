import { create } from "zustand/react";
import VirtualNetworkConnectionModel from "../models/VirtualNetworkConnectionModel.ts";
import virtualNetworkResourceClient from "../api/resourceClient/VirtualNetworkResourceClient.ts";
import { EntityType } from "../models/enums/EntityType.ts";

/**
 * State store for managing network connections between entities on the simulation stage.
 */
interface NetworkConnectionsStore {
    /** Array of all network connections in the simulation */
    networkConnections: VirtualNetworkConnectionModel[];
    /** Fetches all network connections from backend */
    fetch: () => Promise<void>;
    /** Disconnects a network connection by ID */
    disconnectConnection: (id: number) => Promise<void>;
    /** Gets all connections for a specific entity (as source or destination) */
    getConnectionsForEntity: (
        id: number,
        type: EntityType,
    ) => VirtualNetworkConnectionModel[];
}

/**
 * Zustand store hook for managing network connections and virtual links.
 * Tracks all connections between entities and provides query/modification methods.
 */
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
