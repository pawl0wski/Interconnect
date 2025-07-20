import Counter from "./Counter.tsx";
import { render, screen } from "@testing-library/react";
import { userEvent } from "@testing-library/user-event";
import { MantineProvider } from "@mantine/core";

describe("Counter", () => {
    test("should increment", async () => {
        render(
            <MantineProvider>
                <Counter />
            </MantineProvider>
        );

        await userEvent.click(screen.getByText("Click"));

        expect(screen.getByText("Current count: 1")).toBeInTheDocument();
    });
});