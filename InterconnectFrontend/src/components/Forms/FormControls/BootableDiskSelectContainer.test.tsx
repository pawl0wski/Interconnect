import { OperatingSystemType } from "../../../models/enums/OperatingSystemType.ts";
import { render } from "@testing-library/react";
import BootableDiskSelectContainer from "./BootableDiskSelectContainer.tsx";
import { MantineProvider } from "@mantine/core";
import "../../../internalization/i18n.ts";
import { expect } from "vitest";
import { userEvent } from "@testing-library/user-event";

const mockUseBootableDisksStore = vi.hoisted(() => vi.fn());

vi.mock("../../../store/bootableDisksStore.ts", () => ({
    useBootableDisksStore: mockUseBootableDisksStore
}));

describe("BootableDiskSelectContainer", () => {
    test("should fetch bootable disks", () => {
        const mockFetchBootableDisks = vi.fn();
        mockUseBootableDisksStore.mockReturnValue({
            bootableDisks: [],
            fetchBootableDisks: mockFetchBootableDisks
        });

        render(<MantineProvider>
            <BootableDiskSelectContainer
                selectedBootableDiskId={null}
                error={null}
                onBootableDiskChange={vi.fn()}
            />
        </MantineProvider>);

        expect(mockFetchBootableDisks).toHaveBeenCalled();
    });

    test("should display fetched bootable disks", async () => {
        mockUseBootableDisksStore.mockReturnValue({
            bootableDisks: [{
                id: 1,
                name: "TestLinux",
                version: "1.0.4",
                operatingSystemType: OperatingSystemType.Linux
            }],
            fetchBootableDisks: vi.fn()
        });
        const screen = render(<MantineProvider>
            <BootableDiskSelectContainer
                selectedBootableDiskId={null}
                onBootableDiskChange={vi.fn()}
                error={null}
            />
        </MantineProvider>);

        expect(screen.getByText("TestLinux 1.0.4")).toBeInTheDocument();
    });

    test("should display validation error", async () => {
        mockUseBootableDisksStore.mockReturnValue({
            bootableDisks: [],
            fetchBootableDisks: vi.fn()
        });
        const screen = render(<MantineProvider>
            <BootableDiskSelectContainer
                selectedBootableDiskId={null}
                error="TestError"
                onBootableDiskChange={vi.fn()}
            />
        </MantineProvider>);

        expect(screen.getByText("TestError")).toBeInTheDocument();
    });

    test("should select bootable disk when user select bootable disk", async () => {
        const mockOnBootableDiskChange = vi.fn();
        mockUseBootableDisksStore.mockReturnValue({
            bootableDisks: [{
                id: 1,
                name: "TestLinux",
                version: "1.0.4",
                operatingSystemType: OperatingSystemType.Linux
            }],
            fetchBootableDisks: vi.fn()
        });
        const screen = render(<MantineProvider>
            <BootableDiskSelectContainer
                selectedBootableDiskId={null}
                error={null}
                onBootableDiskChange={mockOnBootableDiskChange}
            />
        </MantineProvider>);

        const bootableDiskEntry = screen.getByText("TestLinux 1.0.4");
        await userEvent.click(bootableDiskEntry);

        expect(mockOnBootableDiskChange).toBeCalledWith("1");
    });
});