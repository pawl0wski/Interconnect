import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import CapturedPacketResponse from "../responses/CapturedPacketResponse.ts";

/**
 * Hub client for network packet sniffing using SignalR.
 * Manages real-time packet capture stream from the backend.
 */
class PacketSnifferHubClient extends BaseBackendHubClient {
    /**
     * Joins the default packet sniffing group to start receiving packets.
     * @returns {Promise<void>}
     */
    public async joinDefaultGroup(): Promise<void> {
        return await this.sendHubRequest("JoinDefaultGroup");
    }

    /**
     * Registers a listener for incoming captured network packets.
     * @param {Function} callback Callback invoked when a new packet is captured
     */
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
