import config from "../config/appsettings.json";

interface Configuration {
    BackendUrl: string;
}

const getConfiguration = (): Configuration => {
    return config as Configuration;
};

export { getConfiguration };