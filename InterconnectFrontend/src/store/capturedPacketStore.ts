import { create } from "zustand/react";
import PacketModel from "../models/PacketModel.ts";
import { packetSnifferHubClient } from "../api/hubClient/PacketSnifferHubClient.ts";
import { getConfiguration } from "../configuration.ts";

interface CapturedPacketStore {
    capturedPackets: PacketModel[];
    startCapturingPackets: () => Promise<void>;
    addNewPacket: (packet: PacketModel) => void;
}

const useCapturedPacketStore = create<CapturedPacketStore>()((set, get) => ({
    capturedPackets: [],
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
            packets.sort((x, y) => y.ticks - x.ticks);

            return {
                capturedPackets: packets.slice(0, maxPackets),
            };
        });
    },
}));

export default useCapturedPacketStore;
