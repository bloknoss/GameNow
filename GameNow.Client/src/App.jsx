import { BrowserRouter, Routes, Route } from "react-router-dom";

import Login from "./pages/Login";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";

export default function App() {
    return (
        <main className="">
            <BrowserRouter>
                <Navbar></Navbar>
                <main className="mt-10 flex items-center justify-center">
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/login" element={<Login />} />
                    </Routes>
                </main>
            </BrowserRouter>
        </main>
    );
}
