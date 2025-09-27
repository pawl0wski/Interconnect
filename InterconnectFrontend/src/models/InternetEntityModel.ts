import BaseEntity from "./interfaces/BaseEntity.ts";

interface InternetEntityModel extends BaseEntity {
    ipAddress: string;
}

export default InternetEntityModel;
