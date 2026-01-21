/**
 * Contains information about the connection to the hypervisor.
 */
interface ConnectionInfoModel {
    /** Number of CPU cores available */
    cpuCount: number;
    /** CPU frequency in MHz */
    cpuFreq: number;
    /** Total available memory in bytes */
    totalMemory: number;
    /** URL of the hypervisor connection */
    connectionUrl: string;
    /** Type of the driver used for connection */
    driverType: string;
    /** Version of the libvirt library */
    libVersion: string;
    /** Version of the hypervisor driver */
    driverVersion: string;
    /** Whether the connection info has been successfully fetched */
    connectionInfoFetched: boolean;
}

export default ConnectionInfoModel;
