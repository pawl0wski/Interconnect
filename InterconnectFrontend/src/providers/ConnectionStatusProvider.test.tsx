import { render } from "@testing-library/react";
import ConnectionStatusProvider from "./ConnectionStatusProvider.tsx";
import { expect } from "vitest";

const mockUpdateConnectionStatus = vi.fn();

vi.mock("../store/connectionStore", () => ({
    useConnectionStore: (selector: any) => selector({ updateConnectionStatus: mockUpdateConnectionStatus })
}));

describe("ConnectionStatusProvider", () => {
    test("should fetch connection status on mount", () => {
        render(<ConnectionStatusProvider>
            <div>Test</div>
        </ConnectionStatusProvider>);

        expect(mockUpdateConnectionStatus).toHaveBeenCalledTimes(1);
    });

    test("should fetch connection status in intervals", () => {
        vi.useFakeTimers();

        render(
            <ConnectionStatusProvider>
                <div>test</div>
            </ConnectionStatusProvider>
        );

        expect(mockUpdateConnectionStatus).toHaveBeenCalledTimes(2);

        vi.advanceTimersByTime(5000);
        expect(mockUpdateConnectionStatus).toHaveBeenCalledTimes(3);

        vi.advanceTimersByTime(10000);
        expect(mockUpdateConnectionStatus).toHaveBeenCalledTimes(5);

        vi.useRealTimers();
    });
});