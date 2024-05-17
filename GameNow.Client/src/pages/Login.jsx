import axios from "axios";
import { useState } from "react";

export default function Login() {
    const [formData, setFormData] = useState({ email: "", password: "" });

    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setFormData((values) => ({ ...values, [name]: value }));
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        axios.post("/api/login", formData).then((response) => {
            console.log(response);
            const token = response.data;

            localStorage.setItem("token", token);

            if (token) {
                axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
            }
        });
    };

    return (
        <div className="p-10 bg-slate-200 rounded-xl border w-full max-w-xs">
            <form onSubmit={handleSubmit} className="w-full max-w-lg">
                <div className="flex flex-wrap -mx-3 mb-6">
                    <div className="w-full md:w-2/2 px-3 mb-6 md:mb-0">
                        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" htmlFor="grid-first-name">
                            Email
                        </label>
                        <input
                            className="appearance-none block w-full bg-gray-200 text-gray-700 border border-red-500 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
                            name="email"
                            onChange={handleChange}
                            value={formData.email}
                            type="email"
                            placeholder="example@example.com"
                        />
                        {(formData.email === null || formData.email.length <= 0) && (
                            <p className="text-red-500 text-xs italic">Email is a required field.</p>
                        )}
                    </div>
                </div>
                <div className="flex flex-wrap -mx-3 mb-6">
                    <div className="w-full px-3">
                        <label className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" htmlFor="grid-password">
                            Password
                        </label>
                        <input
                            className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            type="password"
                            placeholder="******************"
                        />
                        {(formData.password === null || formData.password.length === 0) && (
                            <p className="text-red-500 text-xs italic">Password is a required field.</p>
                        )}
                    </div>
                </div>
                <div className="flex flex-wrap -mx-3 mb-3">
                    <div className="w-full px-3">
                        <button
                            className="appearance-none block w-full transition-all bg-blue-500 bold text-black border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-blue-600 hover:bg-blue-700 focus:border-gray-500"
                            id="grid-password"
                            name="password"
                            type="submit"
                            value={formData.password}
                            onChange={handleChange}
                            placeholder="*************"
                        >
                            Login
                        </button>
                    </div>
                </div>
            </form>
        </div>
    );
}
