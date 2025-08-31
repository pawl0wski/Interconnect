import SimulationStage from "./SimulationStage.tsx";
import { useCallback } from "react";
import { useEntityPlacementStore } from "../../store/entityPlacementStore.ts";
import { KonvaEventObject } from "konva/lib/Node";
import SimulationStageEntitiesUtils from "../../utils/simulationStageEntitiesUtils.ts";
import { useSimulationStageContextMenusStore } from "../../store/simulationStageContextMenus.ts";
import { ObjectWithName } from "../../models/interfaces/ObjectWithName.ts";
import { EntityType } from "../../models/enums/EntityType.ts";

const SimulationStageContainer = () => {
    const entityPlacementStore = useEntityPlacementStore();
    const simulationStageContextMenuStore =
        useSimulationStageContextMenusStore();

    const handleOnClick = useCallback(
        (e: KonvaEventObject<MouseEvent>) => {
            if (e.evt.button !== 0) {
                entityPlacementStore.discardPlacingEntity();
                return;
            }

            simulationStageContextMenuStore.clearCurrentContextMenu();
            if (
                entityPlacementStore.currentEntityType ==
                EntityType.VirtualMachine
            ) {
                entityPlacementStore.placeCurrentEntity(e.evt.x, e.evt.y);
            }
        },
        [entityPlacementStore, simulationStageContextMenuStore],
    );

    const handleOnContextMenu = (e: KonvaEventObject<PointerEvent>) => {
        const name = SimulationStageEntitiesUtils.getTargetOrParentEntityInfo(
            e.target as ObjectWithName,
        );

        if (!name) {
            return;
        }

        e.evt.preventDefault();
        const entity = SimulationStageEntitiesUtils.parseShapeName(name);
        const { x, y } = e.evt;
        if (!entity) {
            return;
        }

        simulationStageContextMenuStore.setCurrentContextMenu(
            entity.type,
            entity.id,
            { x, y },
        );
    };

    return (
        <SimulationStage
            onClick={handleOnClick}
            showPlacementCursor={Boolean(
                entityPlacementStore.currentEntityType,
            )}
            onContextMenu={handleOnContextMenu}
        />
    );
};

export default SimulationStageContainer;
