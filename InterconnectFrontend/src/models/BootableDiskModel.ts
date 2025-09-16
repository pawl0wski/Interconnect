import { OperatingSystemType } from "./enums/OperatingSystemType";

interface BootableDiskModel {
    id: number;
    name: string;
    version: string;
    operatingSystemType: OperatingSystemType;
}

export type { BootableDiskModel };
