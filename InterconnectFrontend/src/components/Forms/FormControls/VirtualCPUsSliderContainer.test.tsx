import { render } from "@testing-library/react";
import { MantineProvider } from "@mantine/core";
import VirtualCPUsSliderContainer from "./VirtualCPUsSliderContainer.tsx";
import { userEvent } from "@testing-library/user-event";
import ConnectionInfoModel from "../../../models/ConnectionInfoModel.ts";
import { beforeEach } from "vitest";

const mockUseConnectionInfoStore = vi.hoisted(() => vi.fn());

vi.mock("../../../store/connectionInfoStore.ts", () => ({
    useConnectionInfoStore: mockUseConnectionInfoStore,
}));

describe("VirtualCPUsSliderContainer", () => {
    beforeEach(() => {
        mockUseConnectionInfoStore.mockReset();
    });

    test("should change virtual cpus when user select using slider", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                cpuCount: 5,
            } as ConnectionInfoModel,
        });
        const user = userEvent.setup();
        const mockOnVirtualCPUsChange = vi.fn();
        const screen = render(
            <MantineProvider>
                <VirtualCPUsSliderContainer
                    virtualCPUs={0}
                    error={null}
                    onVirtualCPUsChange={mockOnVirtualCPUsChange}
                />
            </MantineProvider>,
        );

        const sliderThumb = screen.getByRole("slider");
        await user.click(sliderThumb);
        await user.keyboard("{ArrowRight}");

        expect(mockOnVirtualCPUsChange).toHaveBeenLastCalledWith(1);
    });

    test("should display validation error", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                cpuCount: 5,
            } as ConnectionInfoModel,
        });
        const screen = render(
            <MantineProvider>
                <VirtualCPUsSliderContainer
                    virtualCPUs={0}
                    error="TestError"
                    onVirtualCPUsChange={vi.fn()}
                />
            </MantineProvider>,
        );

        expect(screen.getByText("TestError")).toBeInTheDocument();
    });

    test("should display slider label", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                cpuCount: 5,
            } as ConnectionInfoModel,
        });
        const screen = render(
            <MantineProvider>
                <VirtualCPUsSliderContainer
                    virtualCPUs={2}
                    error="TestError"
                    onVirtualCPUsChange={vi.fn()}
                />
            </MantineProvider>,
        );

        const sliderThumb = screen.getByRole("slider");
        await userEvent.hover(sliderThumb);

        expect(screen.getByText("2 CPU")).toBeInTheDocument();
    });
});
