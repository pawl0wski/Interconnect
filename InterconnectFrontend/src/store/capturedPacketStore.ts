import { create } from "zustand/react";
import PacketModel from "../models/PacketModel.ts";
import { packetSnifferHubClient } from "../api/hubClient/PacketSnifferHubClient.ts";
import { getConfiguration } from "../configuration.ts";

interface CapturedPacketStore {
    capturedPackets: PacketModel[];
    hoveredPacket: PacketModel | null;
    startCapturingPackets: () => Promise<void>;
    addNewPacket: (packet: PacketModel) => void;
    setHoveredPacket: (hoveredPacket: PacketModel | null) => void;
}

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
