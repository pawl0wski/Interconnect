import { beforeEach } from "vitest";
import { render } from "@testing-library/react";
import VirtualMachineEntryRenderer from "./VirtualMachineEntryRenderer.tsx";
import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";

const mockVirtualMachineEntitiesStore = vi.hoisted(() => vi.fn());
vi.mock("../../../store/virtualMachineEntitiesStore.ts", () => ({
    useVirtualMachineEntitiesStore: mockVirtualMachineEntitiesStore
}));
vi.mock("../Entity/VirtualMachineEntityContainer.tsx", () => ({
    default: ({ entity }: { entity: VirtualMachineEntityModel }) => <p>{entity.name}</p>
}));


describe("VirtualMachineEntryRenderer", async () => {
    beforeEach(() => {
        mockVirtualMachineEntitiesStore.mockReset();
    });

    test("should fetch virtual machine entries on start", async () => {
        const mockFetchEntities = vi.fn();
        mockVirtualMachineEntitiesStore.mockReturnValueOnce({
            entities: [],
            fetchEntities: mockFetchEntities
        });

        render(
            <VirtualMachineEntryRenderer />
        );

        expect(mockFetchEntities).toHaveBeenCalled();
    });

    test("should render virtual machines", async () => {
        mockVirtualMachineEntitiesStore.mockReturnValueOnce({
            entities: [
                {
                    id: 1,
                    name: "VM1",
                    x: 1,
                    y: 4
                },
                {
                    id: 2,
                    name: "VM2",
                    x: 1,
                    y: 4
                }
            ] as VirtualMachineEntityModel[],
            fetchEntities: vi.fn()
        });

        const screen = render(<VirtualMachineEntryRenderer />);

        expect(screen.getByText(("VM1"))).toBeInTheDocument();
        expect(screen.getByText(("VM2"))).toBeInTheDocument();
    });
});