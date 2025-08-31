import { render } from "@testing-library/react";
import VirtualMachineCreateEntityTrayButtonContainer from "./VirtualMachineCreateEntityTrayButtonContainer.tsx";
import userEvent from "@testing-library/user-event";
import { MantineProvider } from "@mantine/core";
import "../../../internalization/i18n.ts";
import { expect } from "vitest";
import { EntityType } from "../../../models/enums/EntityType.ts";

const mockEntityPlacementStore = vi.hoisted(() => vi.fn());

vi.mock("../../../store/entityPlacementStore.ts", () => ({
    useEntityPlacementStore: mockEntityPlacementStore,
}));

describe("VirtualMachineCreateEntityTrayButtonContainer", () => {
    test("should change current entity to create in store", async () => {
        const mockSetCurrentEntity = vi.fn();
        mockEntityPlacementStore.mockReturnValue({
            setCurrentEntity: mockSetCurrentEntity,
            currentEntityType: null,
        });
        const screen = render(<MantineProvider>
            <VirtualMachineCreateEntityTrayButtonContainer />
        </MantineProvider>);

        const button = screen.getByText("Maszyna wirtualna");
        await userEvent.click(button);

        expect(mockSetCurrentEntity).toHaveBeenCalledWith(EntityType.VirtualMachine);
    });
});