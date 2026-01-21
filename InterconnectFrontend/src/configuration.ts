import config from "../config/appsettings.json";

/**
 * Application configuration interface containing backend connection settings
 * and various runtime limits.
 */
interface Configuration {
    /** URL of the backend API server */
    backendUrl: string;
    /** Maximum safe memory percentage for virtual machines */
    maxSafeVirtualMachineMemoryPercent: number;
    /** Maximum number of captured packets to display at once */
    maxCapturedPacketsAtOnce: number;
}

/**
 * Retrieves the application configuration from the appsettings.json file.
 * @returns {Configuration} The application configuration object
 */
const getConfiguration = (): Configuration => {
    return config as Configuration;
};

export { getConfiguration };
