import { VirtualMachineEntity } from "../models/VirtualMachineEntity";
import { createStore } from "zustand/vanilla";

interface VirtualMachineEntitiesStore {
    entities: VirtualMachineEntity[];
}

const useVirtualMachineEntitiesStore = createStore<VirtualMachineEntitiesStore>()(() => ({
    entities: []
}));

export { useVirtualMachineEntitiesStore };