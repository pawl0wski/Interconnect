import { BrowserRouter, Route, Routes } from "react-router";
import HomePage from "./pages/HomePage.tsx";


const InterconnectRoutes = () => <BrowserRouter>
    <Routes>
        <Route index element={<HomePage />} />
    </Routes>
</BrowserRouter>;

export default InterconnectRoutes;