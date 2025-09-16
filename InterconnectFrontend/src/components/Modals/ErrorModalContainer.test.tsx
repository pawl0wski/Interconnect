import "../../internalization/i18n.ts";
import ErrorModalContainer from "./ErrorModalContainer.tsx";
import { MantineProvider } from "@mantine/core";
import { render } from "@testing-library/react";
import { beforeEach } from "vitest";

const mockUseErrorStore = vi.hoisted(() => vi.fn());

vi.mock("../../store/errorStore.ts", () => ({
    useErrorStore: mockUseErrorStore,
}));

describe("ErrorModalContainer", () => {
    beforeEach(() => {
        mockUseErrorStore.mockReset();
    });

    test("should show error and stacktrace", () => {
        mockUseErrorStore.mockReturnValue({
            error: "Test",
            stackTrace: "TestStackTrace",
        });

        const screen = render(
            <MantineProvider>
                <ErrorModalContainer />
            </MantineProvider>,
        );

        expect(screen.getByText("Wystąpił błąd")).toBeInTheDocument();
        expect(screen.getByText("Test")).toBeInTheDocument();
        expect(screen.getByText("TestStackTrace")).toBeInTheDocument();
    });

    test("should not show modal when there is no error", () => {
        mockUseErrorStore.mockReturnValue({
            error: null,
            stackTrace: null,
        });

        const screen = render(
            <MantineProvider>
                <ErrorModalContainer />
            </MantineProvider>,
        );

        expect(screen.queryByText("Wystąpił błąd")).not.toBeInTheDocument();
    });

    test("should not show textarea when stack trace is not null", () => {
        mockUseErrorStore.mockReturnValue({
            error: "TestError",
            stackTrace: null,
        });

        const screen = render(
            <MantineProvider>
                <ErrorModalContainer />
            </MantineProvider>,
        );

        expect(screen.queryByRole("textbox")).not.toBeInTheDocument();
    });
});
