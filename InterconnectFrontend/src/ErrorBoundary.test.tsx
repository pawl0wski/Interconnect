import ErrorBoundary from "./ErrorBoundary.tsx";
import { render } from "@testing-library/react";
import { userEvent } from "@testing-library/user-event";
import { expect } from "vitest";

const mockUseErrorStore = vi.hoisted(() => vi.fn());

vi.mock("./store/errorStore.ts", () => ({
    useErrorStore: mockUseErrorStore
}));

const TestComponent = () => {
    const throwError = () => {
        const error = new Error("TestError");
        error.stack = "TestStackTrace";
        throw error;
    };

    return <button onClick={throwError}>Throw</button>;
};

describe("ErrorBoundary", () => {
    test("should setError when error occurred in app", async () => {
        const mockSetError = vi.fn();
        mockUseErrorStore.mockReturnValueOnce({
            setError: mockSetError
        });
        const screen = render(<ErrorBoundary><TestComponent /></ErrorBoundary>);

        const button = screen.getByRole("button");
        await userEvent.click(button);

        expect(mockSetError).toHaveBeenCalled();
    });
});