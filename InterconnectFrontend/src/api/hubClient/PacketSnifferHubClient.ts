import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import CapturedPacketResponse from "../responses/CapturedPacketResponse.ts";

class PacketSnifferHubClient extends BaseBackendHubClient {
    public async joinDefaultGroup(): Promise<void> {
        return await this.sendHubRequest("JoinDefaultGroup");
    }

    public startListeningForPackets(
        callback: (resp: CapturedPacketResponse) => void,
    ) {
        return this.startListeningForMessages("NewPacket", callback);
    }

    protected getHubName(): string {
        return "PacketSnifferHub";
    }
}

const packetSnifferHubClient = new PacketSnifferHubClient();

export { packetSnifferHubClient };
