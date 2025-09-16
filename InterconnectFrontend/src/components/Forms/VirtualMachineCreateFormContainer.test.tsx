import { render } from "@testing-library/react";
import { MantineProvider } from "@mantine/core";
import VirtualMachineCreateFormContainer from "./VirtualMachineCreateFormContainer.tsx";
import userEvent from "@testing-library/user-event";
import "../../internalization/i18n.ts";

const mockUseVirtualMachineCreateStore = vi.hoisted(() => vi.fn());
const mockUseVirtualMachineEntitiesStore = vi.hoisted(() => vi.fn());
const mockUseBootableDisksStore = vi.hoisted(() => vi.fn());

vi.mock("../../store/virtualMachineCreateStore.ts", () => ({
    useVirtualMachineCreateStore: mockUseVirtualMachineCreateStore,
}));

vi.mock("../../store/virtualMachineEntitiesStore.ts", () => ({
    useVirtualMachineEntitiesStore: mockUseVirtualMachineEntitiesStore,
}));

vi.mock("../../store/bootableDisksStore.ts", () => ({
    useBootableDisksStore: mockUseBootableDisksStore,
}));

describe("VirtualMachineCreateFormContainer", () => {
    test("should show empty values validation errors", async () => {
        mockUseBootableDisksStore.mockReturnValue({
            bootableDisks: [],
            fetchBootableDisks: vi.fn(),
        });
        const screen = render(
            <MantineProvider>
                <VirtualMachineCreateFormContainer onFormSubmitted={vi.fn()} />
            </MantineProvider>,
        );

        const button = screen.getByText("Stwórz maszynę wirtualną");

        await userEvent.click(button);

        expect(
            screen.getByText("Nazwa nie może być pusta"),
        ).toBeInTheDocument();
        expect(
            screen.getByText("Musisz wybrać przynajmniej jedno wirtualne CPU"),
        ).toBeInTheDocument();
        expect(
            screen.getByText(
                "Musisz zadeklarować pamięć operacyjną dla maszyny wirtualnej",
            ),
        ).toBeInTheDocument();
        expect(
            screen.getByText("Musisz wybrać obraz dla maszyny wirtualnej"),
        ).toBeInTheDocument();
    });
});
