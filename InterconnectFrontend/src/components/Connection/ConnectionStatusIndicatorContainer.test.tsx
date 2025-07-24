import { MantineProvider } from "@mantine/core";
import { beforeEach, expect } from "vitest";
import { render } from "@testing-library/react";
import { ConnectionStatus } from "../../models/enums/ConnectionStatus.ts";
import ConnectionStatusIndicatorContainer from "./ConnectionStatusIndicatorContainer.tsx";
import { userEvent } from "@testing-library/user-event";

const mockUseConnectionStore = vi.hoisted(() => vi.fn());
const mockUseConnectionInfoModalStore = vi.hoisted(() => vi.fn());

vi.mock("../../store/connectionStore.ts", () => ({
    useConnectionStore: mockUseConnectionStore
}));

vi.mock("../../store/modals/connectionInfoModalStore.ts", () => ({
    useConnectionInfoModalStore: mockUseConnectionInfoModalStore
}));

vi.mock("react-icons/md", () => ({
    MdCloudDone: () => <div>CloudDoneIcon</div>,
    MdCloudOff: () => <div>CloudOffIcon</div>,
    MdSync: () => <div>SyncIcon</div>
}));

describe("ConnectionStatusIndicator", () => {
    beforeEach(() => {
        mockUseConnectionStore.mockClear();
    });

    test("should show alive connection status when connection is alive", () => {
        mockUseConnectionStore.mockReturnValue(ConnectionStatus.Alive);

        const screen = render(<MantineProvider>
            <ConnectionStatusIndicatorContainer />
        </MantineProvider>);

        expect(screen.getByText(("Połączono"))).toBeInTheDocument();
        expect(screen.getByText("CloudDoneIcon")).toBeInTheDocument();
    });

    test("should show dead connection status when connection is dead", () => {
        mockUseConnectionStore.mockReturnValue(ConnectionStatus.Dead);

        const screen = render(<MantineProvider>
            <ConnectionStatusIndicatorContainer />
        </MantineProvider>);

        expect(screen.getByText("Brak połączenia")).toBeInTheDocument();
        expect(screen.getByText("CloudOffIcon")).toBeInTheDocument();
    });

    test("should open connection info modal when user press at connection status", async () => {
        const mockOpen = vi.fn();
        mockUseConnectionStore.mockReturnValue(ConnectionStatus.Alive);
        mockUseConnectionInfoModalStore.mockReturnValue({
            open: mockOpen
        });

        const screen = render(<MantineProvider>
            <ConnectionStatusIndicatorContainer />
        </MantineProvider>);

        await userEvent.click(screen.getByText("Połączono"));

        expect(mockOpen).toHaveBeenCalled();
    });
});