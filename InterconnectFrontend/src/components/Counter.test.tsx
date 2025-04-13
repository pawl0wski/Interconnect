import Counter from "./Counter.tsx";
import { render, screen } from "@testing-library/react";
import { userEvent } from "@testing-library/user-event";

describe("Counter", () => {
    test("should increment", async () => {
        render(<Counter />);

        await userEvent.click(screen.getByText("Increment"));

        expect(screen.getByText("Current count: 1")).toBeInTheDocument();
    });
});