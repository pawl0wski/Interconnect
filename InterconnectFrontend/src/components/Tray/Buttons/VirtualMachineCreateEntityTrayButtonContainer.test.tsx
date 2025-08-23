import { render } from "@testing-library/react";
import VirtualMachineCreateEntityTrayButtonContainer from "./VirtualMachineCreateEntityTrayButtonContainer.tsx";
import userEvent from "@testing-library/user-event";
import { MantineProvider } from "@mantine/core";
import "../../../internalization/i18n.ts";

const mockVirtualMachineCreateModalStore = vi.hoisted(() => vi.fn());

vi.mock("../../../store/modals/virtualMachineCreateModalStore.ts", () => ({
    useVirtualMachineCreateModalStore: mockVirtualMachineCreateModalStore
}));

describe("VirtualMachineCreateEntityTrayButtonContainer", () => {
    test("should open virtual machine create modal when button is pressed", async () => {
        const mockOpenModal = vi.fn();
        mockVirtualMachineCreateModalStore.mockReturnValue({
            open: mockOpenModal
        });
        const screen = render(<MantineProvider>
            <VirtualMachineCreateEntityTrayButtonContainer />
        </MantineProvider>);

        const button = screen.getByText("Maszyna wirtualna");
        await userEvent.click(button);

        expect(mockOpenModal).toHaveBeenCalled();
    });
});