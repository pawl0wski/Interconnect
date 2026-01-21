import BaseEntity from "./interfaces/BaseEntity.ts";

/**
 * Represents an Internet entity in the simulation, extending base entity with IP address.
 */
interface InternetEntityModel extends BaseEntity {
    /** IP address of the Internet entity */
    ipAddress: string;
}

export default InternetEntityModel;
