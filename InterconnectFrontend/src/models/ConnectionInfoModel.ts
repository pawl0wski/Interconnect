interface ConnectionInfoModel {
    cpuCount: number;
    cpuFreq: number;
    totalMemory: number;
    connectionUrl: string;
    driverType: string;
    libVersion: string;
    driverVersion: string;
    connectionInfoFetched: boolean;
}

export default ConnectionInfoModel;