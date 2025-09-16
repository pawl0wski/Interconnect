import InputWarning from "./InputWarning.tsx";
import { render } from "@testing-library/react";

vi.mock("react-icons/md", () => ({
    MdWarning: () => <svg data-testid="warning-icon" />,
}));

describe("InputWarning", () => {
    test("should display provided test", () => {
        const screen = render(<InputWarning text="Test Warning" />);

        expect(screen.getByText("Test Warning")).toBeInTheDocument();
    });

    test("should display warning icon", () => {
        const screen = render(<InputWarning text="Test Warning" />);

        expect(screen.getByTestId("warning-icon")).toBeInTheDocument();
    });
});
