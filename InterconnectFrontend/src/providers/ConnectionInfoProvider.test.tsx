import { render } from "@testing-library/react";
import { expect } from "vitest";
import ConnectionInfoProvider from "./ConnectionInfoProvider.tsx";

const mockUpdateConnectionInfo = vi.fn();

vi.mock("../store/connectionInfoStore", () => ({
    useConnectionInfoStore: (selector: any) =>
        selector({ updateConnectionInfo: mockUpdateConnectionInfo }),
}));

describe("ConnectionInfoProvider", () => {
    test("should fetch connection info on mount", () => {
        render(
            <ConnectionInfoProvider>
                <div>Test</div>
            </ConnectionInfoProvider>,
        );

        expect(mockUpdateConnectionInfo).toHaveBeenCalledTimes(1);
    });
});
