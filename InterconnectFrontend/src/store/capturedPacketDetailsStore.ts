import { create } from "zustand/react";
import PacketModel from "../models/PacketModel.ts";

interface CapturedPacketDetailsStore {
    capturedPacket: PacketModel | null;
    setCapturedPacket: (capturedPacket: PacketModel) => void;
    clearCapturedPacket: () => void;
}

const useCapturedPacketDetailsStore = create<CapturedPacketDetailsStore>(
    (set) => ({
        capturedPacket: null,
        setCapturedPacket: (capturedPacket: PacketModel) =>
            set({
                capturedPacket,
            }),
        clearCapturedPacket: () => set({ capturedPacket: null }),
    }),
);

export default useCapturedPacketDetailsStore;
