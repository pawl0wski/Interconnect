import { describe, expect } from "vitest";
import { useErrorStore } from "./errorStore.ts";
import { act, renderHook } from "@testing-library/react";

describe("errorStore", () => {
    test("should set error when setError is called", () => {
        const { result } = renderHook(() => useErrorStore());

        act(() => {
            result.current.setError(new Error("TestError"));
        });

        expect(result.current.error).toBe("TestError");
    });

    test("should set stacktrace when setError is called", () => {
        const error = new Error("TestError");
        error.stack = "TestStackTrace";
        const { result } = renderHook(() => useErrorStore());

        act(() => {
            result.current.setError(error);
        });

        expect(result.current.stackTrace).toBe("TestStackTrace");
    });

    test("should clear error and stacktrace when clearError is called", () => {
        const error = new Error("TestError");
        error.stack = "TestStackTrace";
        const { result } = renderHook(() => useErrorStore());

        act(() => {
            result.current.setError(error);
        });

        expect(result.current.error).toBe("TestError");
        expect(result.current.stackTrace).toBe("TestStackTrace");

        act(() => {
            result.current.clearError();
        });

        expect(result.current.error).toBeNull();
        expect(result.current.stackTrace).toBeNull();
    });

    test("should not store stacktrace when showStackTrace is false", () => {
        const error = new Error("TestError");
        error.stack = "TestStackTrace";
        const { result } = renderHook(() => useErrorStore());

        act(() => {
            result.current.setError(error, { showStackTrace: false });
        });

        expect(result.current.stackTrace).toBeNull();
    });
});
