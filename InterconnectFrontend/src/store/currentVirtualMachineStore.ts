import { create } from "zustand/react";

interface CurrentVirtualMachineStore {
    uuid: string | null;
    setUuid: (uuid: string) => void;
}

const useCurrentVirtualMachineStore = create<CurrentVirtualMachineStore>((set) => ({
    uuid: null,
    setUuid: (uuid: string) => set({ uuid })
}));

export { useCurrentVirtualMachineStore };