/**
 * Represents a bootable disk available in the system.
 */
interface BootableDiskModel {
    /** Unique identifier for the bootable disk */
    id: number;
    /** Name of the bootable disk */
    name: string;
    /** Version of the bootable disk */
    version: string;
}

export type { BootableDiskModel };
