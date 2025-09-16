import { act } from "@testing-library/react";
import { createModalStore } from "./modalStores.ts";

describe("createModalStore", () => {
    let store: ReturnType<typeof createModalStore>;

    beforeEach(() => {
        store = createModalStore();
    });

    it("should have opened=false initially", () => {
        expect(store.getState().opened).toBe(false);
    });

    it("should set opened=true after calling open()", () => {
        act(() => {
            store.getState().open();
        });
        expect(store.getState().opened).toBe(true);
    });

    it("should set opened=false after calling close()", () => {
        act(() => {
            store.getState().open();
        });
        expect(store.getState().opened).toBe(true);

        act(() => {
            store.getState().close();
        });
        expect(store.getState().opened).toBe(false);
    });

    it("should handle multiple open/close calls correctly", () => {
        act(() => {
            store.getState().open();
            store.getState().close();
            store.getState().open();
        });
        expect(store.getState().opened).toBe(true);
    });
});
