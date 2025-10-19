import BaseEntity from "./interfaces/BaseEntity.ts";

interface VirtualNetworkNodeEntityModel extends BaseEntity {
    name: string | null;
    uuid: string;
}

export default VirtualNetworkNodeEntityModel;
