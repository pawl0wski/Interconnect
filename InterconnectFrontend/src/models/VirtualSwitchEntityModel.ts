import BaseEntity from "./interfaces/BaseEntity.ts";

interface VirtualSwitchEntityModel extends BaseEntity {
    name: string | null;
    uuid: string;
}

export default VirtualSwitchEntityModel;