import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import TerminalDataResponse from "../responses/TerminalDataResponse.ts";

class VirtualMachineConsoleHubClient extends BaseBackendHubClient {
    public async joinConsoleGroup(uuid: string): Promise<void> {
        return await this.sendHubRequest("JoinConsoleGroup", uuid);
    }

    public async leaveConsoleGroup(uuid: string): Promise<void> {
        return await this.sendHubRequest("LeaveConsoleGroup", uuid);
    }

    public async sendDataToConsole(uuid: string, data: string): Promise<void> {
        return await this.sendHubRequest("SendDataToConsole", { uuid, data });
    }

    public async getInitialDataForConsole(
        uuid: string,
    ): Promise<TerminalDataResponse> {
        return await this.sendHubRequest("GetInitialDataForConsole", uuid);
    }

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
