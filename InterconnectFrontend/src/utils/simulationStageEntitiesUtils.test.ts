import SimulationStageEntitiesUtils from "./simulationStageEntitiesUtils.ts";
import { expect } from "vitest";
import { ObjectWithId } from "../models/interfaces/ObjectWithId.ts";
import { EntityType } from "../models/enums/EntityType.ts";

describe("SimulationStageEntitiesUtils", () => {
    test("getTargetOrParentEntityInfo should get entity info from target", () => {
        const target = {
            name: () => "test",
            parent: undefined,
        };

        const info =
            SimulationStageEntitiesUtils.getTargetOrParentEntityInfo(target);

        expect(info).toBe("test");
    });
    test("getTargetOrParentEntityInfo should get entity info from parent", () => {
        const target = {
            name: () => undefined,
            parent: {
                name: () => "testParent",
                parent: undefined,
            },
        };

        const info =
            SimulationStageEntitiesUtils.getTargetOrParentEntityInfo(target);

        expect(info).toBe("testParent");
    });
    test("getTargetOrParentEntityInfo should return null if there is no name in target and its parents", () => {
        const target = {
            name: () => undefined,
            parent: {
                name: () => undefined,
                parent: undefined,
            },
        };

        const info =
            SimulationStageEntitiesUtils.getTargetOrParentEntityInfo(target);

        expect(info).toBeNull();
    });
    test("createShapeName should create shape name using id and type", () => {
        const entity = {
            id: 123,
        } as ObjectWithId;

        const name = SimulationStageEntitiesUtils.createShapeName(
            entity,
            EntityType.VirtualMachine,
        );

        expect(name).toBe("vm-123");
    });
    test("parseShapeName should get id and type from entity name", () => {
        const { type, id } =
            SimulationStageEntitiesUtils.parseShapeName("vm-123")!;

        expect(type).toBe(EntityType.VirtualMachine);
        expect(id).toBe(123);
    });
    test("parseShapeName should return unknown if entity name can not be resolved", () => {
        const returnValue =
            SimulationStageEntitiesUtils.parseShapeName("gfds-123");

        expect(returnValue).toBeNull();
    });
});
