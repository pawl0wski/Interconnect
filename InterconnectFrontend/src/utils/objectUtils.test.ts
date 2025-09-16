import ObjectUtils from "./objectUtils.ts";
import { expect } from "vitest";

describe("ObjectUtils", () => {
    test("should get value when value is defined in record", () => {
        const result = ObjectUtils.getValueOrNull(
            { testKey: "testValue" },
            "testKey",
        );

        expect(result).toBe("testValue");
    });

    test("should get null when value is not defined in record", () => {
        const result = ObjectUtils.getValueOrNull({}, "testKey");

        expect(result).toBeNull();
    });
});
