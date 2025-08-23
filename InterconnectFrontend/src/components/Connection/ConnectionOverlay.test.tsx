import { MantineProvider } from "@mantine/core";
import { render } from "@testing-library/react";
import ConnectionOverlay from "./ConnectionOverlay.tsx";
import { ConnectionStatus } from "../../models/enums/ConnectionStatus.ts";
import "../../internalization/i18n.ts";

const mockUseConnectionStore = vi.hoisted(() => vi.fn());
vi.mock("../../store/connectionStore.ts", () => ({
    useConnectionStore: mockUseConnectionStore
}));

describe("ConnectionOverlay", () => {
    test("should show overlay if connection is unknown", () => {
        mockUseConnectionStore.mockReturnValue(ConnectionStatus.Unknown);

        const screen = render(<MantineProvider>
            <ConnectionOverlay />
        </MantineProvider>);

        expect(screen.getByText("Łączenie z serwerem...")).toBeInTheDocument();
    });

    test.each(
        [ConnectionStatus.Alive, ConnectionStatus.Dead]
    )("should not show overlay if connection is not unknown",
        (status: ConnectionStatus) => {
            mockUseConnectionStore.mockReturnValue(status);

            const screen = render(<MantineProvider>
                <ConnectionOverlay />
            </MantineProvider>);

            expect(screen.queryByText("Łączenie z serwerem...")).not.toBeInTheDocument();
        });
});