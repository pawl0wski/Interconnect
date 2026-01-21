import { create } from "zustand/react";
import PacketModel from "../models/PacketModel.ts";

/**
 * State store for managing the currently selected packet for detailed inspection.
 */
interface CapturedPacketDetailsStore {
    /** Currently selected packet for detailed view, or null if none selected */
    capturedPacket: PacketModel | null;
    /** Sets the packet to display in the detail modal */
    setCapturedPacket: (capturedPacket: PacketModel) => void;
    /** Clears the currently selected packet */
    clearCapturedPacket: () => void;
}

/**
 * Zustand store hook for managing the selected packet details.
 * Provides state for displaying a single packet in detailed inspection mode.
 */
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
