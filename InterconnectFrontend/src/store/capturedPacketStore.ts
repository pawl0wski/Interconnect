import { create } from "zustand/react";
import PacketModel from "../models/PacketModel.ts";
import { packetSnifferHubClient } from "../api/hubClient/PacketSnifferHubClient.ts";
import { getConfiguration } from "../configuration.ts";

/**
 * State store for managing captured network packets.
 */
interface CapturedPacketStore {
    /** Array of captured packets (limited by maxCapturedPacketsAtOnce) */
    capturedPackets: PacketModel[];
    /** Currently hovered packet for highlighting, or null */
    hoveredPacket: PacketModel | null;
    /** Starts listening for packets from the PacketSnifferHub */
    startCapturingPackets: () => Promise<void>;
    /** Adds a new packet to the store, removing oldest if limit exceeded */
    addNewPacket: (packet: PacketModel) => void;
    /** Sets the currently hovered packet */
    setHoveredPacket: (hoveredPacket: PacketModel | null) => void;
}

/**
 * Zustand store hook for managing network packet capturing.
 * Maintains a limited buffer of most recent captured packets.
 */
const useCapturedPacketStore = create<CapturedPacketStore>()((set, get) => ({
    capturedPackets: [],
    hoveredPacket: null,
    startCapturingPackets: async () => {
        await packetSnifferHubClient.joinDefaultGroup();
        packetSnifferHubClient.startListeningForPackets((resp) =>
            get().addNewPacket(resp.data),
        );
    },
    addNewPacket(packet: PacketModel) {
        set((state) => {
            const maxPackets = getConfiguration().maxCapturedPacketsAtOnce;
            const packets = [...state.capturedPackets, packet];
            packets.sort((x, y) => y.id - x.id);

            return {
                capturedPackets: packets.slice(0, maxPackets),
            };
        });
    },
    setHoveredPacket: (hoveredPacket: PacketModel | null) => {
        set({
            hoveredPacket: hoveredPacket,
        });
    },
}));

export default useCapturedPacketStore;
