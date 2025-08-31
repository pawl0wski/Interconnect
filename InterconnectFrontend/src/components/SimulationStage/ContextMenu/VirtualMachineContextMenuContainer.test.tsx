import { fireEvent, render } from "@testing-library/react";
import { beforeEach, describe, expect, test, vi } from "vitest";
import VirtualMachineContextMenuContainer from "./VirtualMachineContextMenuContainer.tsx";

const {
    mockSimulationStageContextMenusStore,
    mockVirtualMachineEntitiesStore,
    mockCurrentVirtualMachineModalStore,
    mockCurrentVirtualMachineStore,
    mockUseSimulationStageContextMenuInfo,
    mockUseSimulationStageContextMenuClose
} = vi.hoisted(() => ({
    mockSimulationStageContextMenusStore: { currentEntityId: 1 as number | undefined },
    mockVirtualMachineEntitiesStore: { getById: vi.fn(() => ({ id: 1, name: "TestVM" })) },
    mockCurrentVirtualMachineModalStore: { open: vi.fn() },
    mockCurrentVirtualMachineStore: { setCurrentEntity: vi.fn() },
    mockUseSimulationStageContextMenuInfo: { position: { x: 10, y: 20 }, visible: true },
    mockUseSimulationStageContextMenuClose: { closeContextMenu: vi.fn() }
}));

vi.mock("../../../store/simulationStageContextMenus.ts", () => ({
    useSimulationStageContextMenusStore: () => mockSimulationStageContextMenusStore
}));
vi.mock("../../../store/virtualMachineEntitiesStore.ts", () => ({
    useVirtualMachineEntitiesStore: () => mockVirtualMachineEntitiesStore
}));
vi.mock("../../../store/currentVirtualMachineStore.ts", () => ({
    useCurrentVirtualMachineStore: () => mockCurrentVirtualMachineStore
}));
vi.mock("../../../store/modals/currentVirtualMachineModalStore.ts", () => ({
    useCurrentVirtualMachineModalStore: () => mockCurrentVirtualMachineModalStore
}));
vi.mock("../../../hooks/useSimulationStageContextMenuInfo.ts", () => ({
    useSimulationStageContextMenuInfo: () => mockUseSimulationStageContextMenuInfo
}));
vi.mock("../../../hooks/useSimulationStageContextMenuClose.ts", () => ({
    default: () => mockUseSimulationStageContextMenuClose
}));

vi.mock("./VirtualMachineContextMenu.tsx", () => ({
    default: ({ title, isVisible, openVirtualMachineConsole }: any) => (
        <div data-testid="context-menu" data-title={title} data-visible={isVisible} onClick={openVirtualMachineConsole}>
            ContextMenu
        </div>
    )
}));

describe("VirtualMachineContextMenuContainer", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    test("should call setCurrentEntity, open modal and close context menu when openVirtualMachineConsole is called", () => {
        const { getByTestId } = render(<VirtualMachineContextMenuContainer />);
        const menu = getByTestId("context-menu");

        fireEvent.click(menu);

        expect(mockCurrentVirtualMachineStore.setCurrentEntity).toHaveBeenCalledWith({ id: 1, name: "TestVM" });
        expect(mockCurrentVirtualMachineModalStore.open).toHaveBeenCalled();
        expect(mockUseSimulationStageContextMenuClose.closeContextMenu).toHaveBeenCalled();
    });

    test("should handle case when currentEntity is undefined", () => {
        mockSimulationStageContextMenusStore.currentEntityId = undefined;
        const { getByTestId } = render(<VirtualMachineContextMenuContainer />);
        const menu = getByTestId("context-menu");

        fireEvent.click(menu);

        expect(mockCurrentVirtualMachineStore.setCurrentEntity).not.toHaveBeenCalled();
        expect(mockCurrentVirtualMachineModalStore.open).not.toHaveBeenCalled();
        expect(mockUseSimulationStageContextMenuClose.closeContextMenu).not.toHaveBeenCalled();
    });
});
