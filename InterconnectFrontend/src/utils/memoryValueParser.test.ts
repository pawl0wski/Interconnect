import MemoryValueParser from "./memoryValueParser.ts";
import { expect } from "vitest";

describe("MemoryValueParser", () => {
    test.each([
        { value: 1, expectedHumanReadable: "1KB" },
        { value: 1024, expectedHumanReadable: "1MB" },
        { value: 2048, expectedHumanReadable: "2MB" },
        { value: 1024 * 1024, expectedHumanReadable: "1GB" },
        { value: 512, expectedHumanReadable: "512KB" },
        { value: 3 * 1024 * 1024, expectedHumanReadable: "3GB" },
    ])(
        "should parse value to human readable format",
        ({ value, expectedHumanReadable }) => {
            const result = MemoryValueParser.valueMemoryToHumanReadable(value);
            expect(result).toBe(expectedHumanReadable);
        },
    );

    test.each([
        { humanReadable: "1MB", expectedResult: 1024 },
        { humanReadable: "2MB", expectedResult: 2048 },
        { humanReadable: "1GB", expectedResult: 1024 * 1024 },
        { humanReadable: "512KB", expectedResult: 512 },
        { humanReadable: "0.5MB", expectedResult: 512 },
    ])(
        "should parse human readable to value",
        ({ humanReadable, expectedResult }) => {
            const result =
                MemoryValueParser.humanReadableMemoryToValue(humanReadable);
            expect(result).toBe(expectedResult);
        },
    );
});
