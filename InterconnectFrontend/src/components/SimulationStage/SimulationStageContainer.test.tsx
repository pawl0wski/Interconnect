import { fireEvent, render } from "@testing-library/react";
import { beforeEach, describe, expect, test, vi } from "vitest";
import SimulationStageContainer from "./SimulationStageContainer";
import SimulationStageEntitiesUtils from "../../utils/simulationStageEntitiesUtils.ts";
import { EntityType } from "../../models/enums/EntityType.ts";

const { mockEntityPlacementStore, mockContextMenuStore } = vi.hoisted(() => ({
    mockEntityPlacementStore: {
        currentEntityType: null as EntityType | null,
        placeCurrentEntity: vi.fn(),
        discardPlacingEntity: vi.fn(),
        setCurrentEntity: vi.fn()
    },
    mockContextMenuStore: {
        currentEntityType: null as EntityType | null,
        currentEntityId: null,
        currentPosition: { x: 0, y: 0 },
        setCurrentContextMenu: vi.fn(),
        clearCurrentContextMenu: vi.fn()
    }
}));

vi.mock("../../store/entityPlacementStore.ts", () => ({
    useEntityPlacementStore: () => mockEntityPlacementStore
}));

vi.mock("../../store/simulationStageContextMenus.ts", () => ({
    useSimulationStageContextMenusStore: () => mockContextMenuStore
}));

vi.mock("./SimulationStage.tsx", () => ({
    default: ({ onClick, onContextMenu, showPlacementCursor }: any) => (
        <div data-testid="stage"
             onClick={(e) =>
                 onClick({ evt: { button: e.button, x: e.clientX, y: e.clientY } })
             }
             onContextMenu={(e) => onContextMenu({
                 evt: {
                     x: e.clientX, y: e.clientY, preventDefault: () => {
                     }
                 }, target: { name: "entity-1" }
             })}
        >
            {showPlacementCursor ? "cursor" : null}
        </div>
    )
}));


vi.mock("../../utils/simulationStageEntitiesUtils.ts", () => ({
    default: {
        getTargetOrParentEntityInfo: vi.fn(() => "entity-1"),
        parseShapeName: vi.fn(() => ({ type: EntityType.VirtualMachine, id: 1 }))
    }
}));

describe("SimulationStageContainer", () => {
    beforeEach(() => {
        vi.clearAllMocks();
        mockEntityPlacementStore.currentEntityType = null;
    });

    test("should clear context menu and place entity on left click", () => {
        mockEntityPlacementStore.currentEntityType = EntityType.VirtualMachine;
        const { getByTestId } = render(<SimulationStageContainer />);
        const stage = getByTestId("stage");

        fireEvent.click(stage, { button: 0, clientX: 50, clientY: 60 });

        expect(mockContextMenuStore.clearCurrentContextMenu).toHaveBeenCalled();
        expect(mockEntityPlacementStore.placeCurrentEntity).toHaveBeenCalledWith(50, 60);
    });

    test("should set current context menu on right click over entity", () => {
        const { getByTestId } = render(<SimulationStageContainer />);
        const stage = getByTestId("stage");

        fireEvent.contextMenu(stage);

        expect(SimulationStageEntitiesUtils.getTargetOrParentEntityInfo).toHaveBeenCalled();
        expect(SimulationStageEntitiesUtils.parseShapeName).toHaveBeenCalled();
        expect(mockContextMenuStore.setCurrentContextMenu).toHaveBeenCalledWith(EntityType.VirtualMachine, 1, {
            x: 0,
            y: 0
        });
    });

    test("should show placement cursor when currentEntityType is set", () => {
        mockEntityPlacementStore.currentEntityType = EntityType.VirtualMachine;
        const { getByTestId } = render(<SimulationStageContainer />);
        const stage = getByTestId("stage");

        expect(stage.textContent).toContain("cursor");
    });

    test("should not show placement cursor when currentEntityType is null", () => {
        const { getByTestId } = render(<SimulationStageContainer />);
        const stage = getByTestId("stage");

        expect(stage.textContent).not.toContain("cursor");
    });
});
