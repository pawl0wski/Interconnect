import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import TerminalDataResponse from "../responses/TerminalDataResponse.ts";

/**
 * Hub client for virtual machine console communication using SignalR.
 * Manages real-time terminal data streaming from virtual machines.
 */
class VirtualMachineConsoleHubClient extends BaseBackendHubClient {
    /**
     * Joins a console group for a specific virtual machine.
     * @param {string} uuid The UUID of the virtual machine
     * @returns {Promise<void>}
     */
    public async joinConsoleGroup(uuid: string): Promise<void> {
        return await this.sendHubRequest("JoinConsoleGroup", uuid);
    }

    /**
     * Leaves the console group for a specific virtual machine.
     * @param {string} uuid The UUID of the virtual machine
     * @returns {Promise<void>}
     */
    public async leaveConsoleGroup(uuid: string): Promise<void> {
        return await this.sendHubRequest("LeaveConsoleGroup", uuid);
    }

    /**
     * Sends input data to the virtual machine console.
     * @param {string} uuid The UUID of the virtual machine
     * @param {string} data The data/command to send to the console
     * @returns {Promise<void>}
     */
    public async sendDataToConsole(uuid: string, data: string): Promise<void> {
        return await this.sendHubRequest("SendDataToConsole", { uuid, data });
    }

    /**
     * Retrieves initial terminal data for a virtual machine console.
     * @param {string} uuid The UUID of the virtual machine
     * @returns {Promise<TerminalDataResponse>} The initial console data
     */
    public async getInitialDataForConsole(
        uuid: string,
    ): Promise<TerminalDataResponse> {
        return await this.sendHubRequest("GetInitialDataForConsole", uuid);
    }

    /**
     * Registers a listener for new terminal data from virtual machines.
     * @param {Function} callback Callback invoked when new terminal data is received
     */
    public startListeningForNewTerminalData(
        callback: (resp: TerminalDataResponse) => void,
    ) {
        return this.startListeningForMessages("NewTerminalData", callback);
    }

    protected getHubName(): string {
        return "VirtualMachineConsoleHub";
    }
}

const virtualMachineConsoleHubClient = new VirtualMachineConsoleHubClient();

export { virtualMachineConsoleHubClient };
