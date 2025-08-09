import StringUtils from "./stringUtils.ts";
import { expect } from "vitest";

describe("StringUtils", () => {
    test.each([
        { input: "this is an example text", expected: "This is an example text" },
        { input: "Already Capitalized", expected: "Already Capitalized" },
        { input: "sINGLE wORD", expected: "SINGLE wORD" },
        { input: "123start with number", expected: "123start with number" },
        { input: "", expected: "" },
        { input: " ", expected: " " },
    ])("should capitalize first letter of the string", ({ input, expected }) => {
        const result = StringUtils.capitalizeString(input);
        expect(result).toBe(expected);
    });
});