import "./internalization/i18n.ts";
import "@xterm/xterm/css/xterm.css";
import "@xterm/xterm/lib/xterm.js";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import ErrorBoundary from "./ErrorBoundary.tsx";

createRoot(document.getElementById("root")!).render(
    <ErrorBoundary>
        <App />
    </ErrorBoundary>
);
