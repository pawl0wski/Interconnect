import { render } from "@testing-library/react";
import { MantineProvider } from "@mantine/core";
import ConnectionInfoModalContainer from "./ConnectionInfoModalContainer.tsx";
import { beforeEach, expect } from "vitest";
import { userEvent } from "@testing-library/user-event";

const mockUseConnectionInfoModalStore = vi.hoisted(() => vi.fn());
const mockUseConnectionInfoStore = vi.hoisted(() => vi.fn());

vi.mock("../../store/connectionInfoStore.ts", () => ({
    useConnectionInfoStore: mockUseConnectionInfoStore
}));
vi.mock("../../store/modals/connectionInfoModalStore.ts", () => ({
    useConnectionInfoModalStore: mockUseConnectionInfoModalStore
}));

describe("ConnectionInfoModalContainer", () => {
    beforeEach(() => {
        mockUseConnectionInfoStore.mockClear();
        mockUseConnectionInfoModalStore.mockClear();
    });

    test("should fetch connection info when connection info is not fetched and modal is opened", () => {
        const mockUpdateConnectionInfo = vi.fn();
        mockUseConnectionInfoModalStore.mockReturnValue({ opened: true });
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: null,
            updateConnectionInfo: mockUpdateConnectionInfo
        });

        render(<MantineProvider>
            <ConnectionInfoModalContainer />
        </MantineProvider>);

        expect(mockUpdateConnectionInfo).toHaveBeenCalled();
    });

    test("should not fetch connection info when modal is closed", () => {
        const mockUpdateConnectionInfo = vi.fn();
        mockUseConnectionInfoModalStore.mockReturnValue({ opened: false });
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: null,
            updateConnectionInfo: mockUpdateConnectionInfo
        });

        render(<MantineProvider>
            <ConnectionInfoModalContainer />
        </MantineProvider>);

        expect(mockUpdateConnectionInfo).not.toHaveBeenCalled();
    });

    test("should not fetch connection info when connection info is already fetched", () => {
        const mockUpdateConnectionInfo = vi.fn();
        mockUseConnectionInfoModalStore.mockReturnValue({ opened: true });
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: { a: "b" },
            updateConnectionInfo: mockUpdateConnectionInfo
        });

        render(<MantineProvider>
            <ConnectionInfoModalContainer />
        </MantineProvider>);

        expect(mockUpdateConnectionInfo).not.toHaveBeenCalled();
    });

    test("should clear connection info and close modal when user close modal", async () => {
        const mockCloseModal = vi.fn();
        const mockClearConnectionInfo = vi.fn();
        mockUseConnectionInfoModalStore.mockReturnValue({ opened: true, close: mockCloseModal });
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: { a: "b" },
            clearConnectionInfo: mockClearConnectionInfo
        });

        const screen = render(<MantineProvider>
            <ConnectionInfoModalContainer />
        </MantineProvider>);

        await userEvent.click(screen.getByRole("button"));

        expect(mockCloseModal).toHaveBeenCalled();
        expect(mockClearConnectionInfo).toHaveBeenCalled();
    });

    test("should display connection info", () => {
        mockUseConnectionInfoModalStore.mockReturnValue({ opened: true });
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: { testKey: "testValue" }
        });

        const screen = render(<MantineProvider>
            <ConnectionInfoModalContainer />
        </MantineProvider>);

        expect(screen.getByText("testKey")).toBeInTheDocument();
        expect(screen.getByText("testValue")).toBeInTheDocument();
    });
});