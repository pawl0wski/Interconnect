import config from "../config/appsettings.json";

interface Configuration {
    backendUrl: string;
    maxSafeVirtualMachineMemoryPercent: number;
    maxCapturedPacketsAtOnce: number;
}

const getConfiguration = (): Configuration => {
    return config as Configuration;
};

export { getConfiguration };
