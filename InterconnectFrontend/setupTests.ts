/**
 * File responsible for importing the basic libraries needed for testing
 */

import "@testing-library/jest-dom";
import { vi } from "vitest";

/*
    Mocks for Mantine to work with vitest
    Docs: https://mantine.dev/guides/vitest/
 */
const { getComputedStyle } = window;
window.getComputedStyle = (elt) => getComputedStyle(elt);
window.HTMLElement.prototype.scrollIntoView = () => {
};

Object.defineProperty(window, "matchMedia", {
    writable: true,
    value: vi.fn().mockImplementation((query) => ({
        matches: false,
        media: query,
        onchange: null,
        addListener: vi.fn(),
        removeListener: vi.fn(),
        addEventListener: vi.fn(),
        removeEventListener: vi.fn(),
        dispatchEvent: vi.fn()
    }))
});

class ResizeObserver {
    observe() {
    }

    unobserve() {
    }

    disconnect() {
    }
}

window.ResizeObserver = ResizeObserver;