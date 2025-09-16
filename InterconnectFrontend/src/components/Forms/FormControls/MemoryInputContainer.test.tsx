import "../../../internalization/i18n.ts";
import ConnectionInfoModel from "../../../models/ConnectionInfoModel.ts";
import MemoryInputContainer from "./MemoryInputContainer.tsx";
import { MantineProvider } from "@mantine/core";
import { beforeEach } from "vitest";
import { render } from "@testing-library/react";
import { userEvent } from "@testing-library/user-event";
import { useState } from "react";

const mockUseConnectionInfoStore = vi.hoisted(() => vi.fn());

vi.mock("../../../store/connectionInfoStore.ts", () => ({
    useConnectionInfoStore: mockUseConnectionInfoStore,
}));

vi.mock("../../../configuration.ts", () => ({
    getConfiguration: () => ({ maxSafeVirtualMachineMemoryPercent: 0.4 }),
}));

const MemoryWrapperWithState = () => {
    const [memory, setMemory] = useState(0);
    return (
        <MantineProvider>
            <MemoryInputContainer
                memory={memory}
                error={null}
                onMemoryChange={setMemory}
            />
        </MantineProvider>
    );
};

describe("MemoryInputContainer", () => {
    beforeEach(() => {
        mockUseConnectionInfoStore.mockReset();
    });

    test("should change memory using slider", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                totalMemory: 1024,
            } as ConnectionInfoModel,
        });
        const user = userEvent.setup();
        const mockOnMemoryChange = vi.fn();
        const screen = render(
            <MantineProvider>
                <MemoryInputContainer
                    memory={0}
                    error={null}
                    onMemoryChange={mockOnMemoryChange}
                />
            </MantineProvider>,
        );

        const sliderThumb = screen.getByRole("slider");
        await user.click(sliderThumb);
        await user.keyboard("{ArrowRight}");

        expect(mockOnMemoryChange).toHaveBeenLastCalledWith(1);
    });

    test("should change memory using input", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                totalMemory: 1073741824,
            } as ConnectionInfoModel,
        });
        const mockOnMemoryChange = vi.fn();
        const screen = render(
            <MantineProvider>
                <MemoryInputContainer
                    memory={0}
                    error={null}
                    onMemoryChange={mockOnMemoryChange}
                />
            </MantineProvider>,
        );

        const inputElement = screen.getByRole("textbox");
        await userEvent.type(
            inputElement,
            "{backspace}{backspace}{backspace}512Mb",
        );

        expect(mockOnMemoryChange).toHaveBeenLastCalledWith(524288);
    });

    test("should display warning when user hit max safe memory limit", () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                totalMemory: 1024,
            } as ConnectionInfoModel,
        });
        const screen = render(
            <MantineProvider>
                <MemoryInputContainer
                    memory={512}
                    error={null}
                    onMemoryChange={vi.fn()}
                />
            </MantineProvider>,
        );

        const warningElement = screen.getByText(
            "Chcesz zadeklarować więcej niż 40% dostępnej pamięci operacyjnej. Może to spowodować spowolnienie działania systemu gospodarza lub innych maszyn wirtualnych.",
        );

        expect(warningElement).toBeInTheDocument();
    });

    test("should update input when user use slider", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                totalMemory: 1024,
            } as ConnectionInfoModel,
        });
        const user = userEvent.setup();
        const screen = render(
            <MantineProvider>
                <MemoryWrapperWithState />
            </MantineProvider>,
        );

        const sliderThumb = screen.getByRole("slider");
        await user.click(sliderThumb);
        await user.keyboard("{ArrowRight}");

        const inputElement = screen.getByRole("textbox");
        expect(inputElement).toHaveValue("1KB");
    });

    test("should show memory label", async () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                totalMemory: 1024,
            } as ConnectionInfoModel,
        });
        const screen = render(
            <MantineProvider>
                <MemoryInputContainer
                    memory={512}
                    error={null}
                    onMemoryChange={vi.fn()}
                />
            </MantineProvider>,
        );

        const sliderThumb = screen.getByRole("slider");
        await userEvent.hover(sliderThumb);

        expect(screen.getByText("512KB")).toBeInTheDocument();
    });

    test("should display validation error", () => {
        mockUseConnectionInfoStore.mockReturnValue({
            connectionInfo: {
                totalMemory: 1024,
            } as ConnectionInfoModel,
        });
        const screen = render(
            <MantineProvider>
                <MemoryInputContainer
                    memory={0}
                    error="TestError"
                    onMemoryChange={vi.fn()}
                />
            </MantineProvider>,
        );

        expect(screen.getByText("TestError")).toBeInTheDocument();
    });
});
