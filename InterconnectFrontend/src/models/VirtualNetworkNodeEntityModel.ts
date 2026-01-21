import BaseEntity from "./interfaces/BaseEntity.ts";

/**
 * Represents a virtual network node entity in the simulation.
 */
interface VirtualNetworkNodeEntityModel extends BaseEntity {
    /** Display name of the network node or null if not set */
    name: string | null;
    /** UUID of the virtual network node */
    uuid: string;
}

export default VirtualNetworkNodeEntityModel;
