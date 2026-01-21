import { BrowserRouter, Route, Routes } from "react-router";
import HomePage from "./pages/HomePage.tsx";

/**
 * Application routing component that defines all routes and their corresponding components.
 * Currently configured with a single home page route as the index.
 * @returns {JSX.Element} Router with all route configurations
 */
const InterconnectRoutes = () => (
    <BrowserRouter>
        <Routes>
            <Route index element={<HomePage />} />
        </Routes>
    </BrowserRouter>
);

export default InterconnectRoutes;
