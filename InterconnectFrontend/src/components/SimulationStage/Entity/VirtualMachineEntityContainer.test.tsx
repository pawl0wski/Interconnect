import { VirtualMachineEntityModel } from "../../../models/VirtualMachineEntityModel.ts";
import { fireEvent, render } from "@testing-library/react";
import { Layer, Stage } from "react-konva";
import VirtualMachineEntityContainer from "./VirtualMachineEntityContainer.tsx";

const mockVirtualMachineEntitiesStore = vi.hoisted(() => vi.fn());
vi.mock("../../../store/virtualMachineEntitiesStore.ts", () => ({
    useVirtualMachineEntitiesStore: mockVirtualMachineEntitiesStore
}));

describe("VirtualMachineEntityContainer", () => {
    test("should update entity position when user drag entity", () => {
        const testEntity = {
            id: 1,
            name: "VM1",
            x: 12,
            y: 43
        } as VirtualMachineEntityModel;
        const mockUpdateEntityPosition = vi.fn();
        mockVirtualMachineEntitiesStore.mockReturnValueOnce(({
            entities: [testEntity],
            updateEntityPosition: mockUpdateEntityPosition
        }));

        const screen = render(<Stage data-testid="test-canvas" width={1000} height={1000}>
            <Layer>
                <VirtualMachineEntityContainer entity={testEntity} />
            </Layer>
        </Stage>);
        const canvas = screen.getByRole("presentation");

        fireEvent.mouseDown(canvas, { clientX: 12, clientY: 43 });
        fireEvent.mouseMove(canvas, { clientX: 100, clientY: 150 });
        fireEvent.mouseUp(canvas);

        expect(mockUpdateEntityPosition).toHaveBeenCalledWith(1, 100, 150);
    });
});