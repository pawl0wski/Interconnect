import { render } from "@testing-library/react";
import { MantineProvider } from "@mantine/core";
import ConnectionInfoModalContainer from "./ConnectionInfoModalContainer.tsx";
import { beforeEach, expect } from "vitest";

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