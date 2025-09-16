import { useSimulationStageContextMenusStore } from "../store/simulationStageContextMenus.ts";
import { useEffect, useState } from "react";
import { PositionModel } from "../models/PositionModel.ts";
import { EntityType } from "../models/enums/EntityType.ts";

interface UseSimulationStageContextMenuReturnType {
    position: PositionModel;
    visible: boolean;
}

export const useSimulationStageContextMenuInfo = (
    entityType: EntityType,
): UseSimulationStageContextMenuReturnType => {
    const simulationStageContextMenuStore =
        useSimulationStageContextMenusStore();
    const [position, setPosition] = useState<PositionModel>({ x: 0, y: 0 });
    const [visible, setVisible] = useState<boolean>(false);

    useEffect(() => {
        if (simulationStageContextMenuStore.currentEntityType == entityType) {
            setPosition(simulationStageContextMenuStore.currentPosition);
            setVisible(true);
            return;
        }
        setVisible(false);
    }, [
        entityType,
        simulationStageContextMenuStore.currentEntityType,
        simulationStageContextMenuStore.currentPosition,
    ]);

    return { position, visible };
};
