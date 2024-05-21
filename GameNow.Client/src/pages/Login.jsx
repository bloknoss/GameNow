import axios from "axios";
import { Facebook, Google, GitHub } from "@mui/icons-material";
import { useState } from "react";
import GoogleButton from "react-google-button";

export default function Login() {
  const [formData, setFormData] = useState({ email: "", password: "" });

  const handleChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setFormData((values) => ({ ...values, [name]: value }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    axios.post("/api/auth/login", formData).then((response) => {
      console.log(response);
      const token = response.data;

      localStorage.setItem("token", token);

      if (token) {
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      }
    });
  };

  return (
    <div
      style={{ backgroundImage: 'url("")' }}
      className=" mt-10 shadow-md p-16 min-w-[30rem] bg-white rounded-xl"
    >
      <div className="text-center">
        <h4 className="mb-12 mt-1 pb-1 text-xl font-semibold">
          Iniciar Sesión
        </h4>
      </div>

      <form onSubmit={handleSubmit} className="w-full max-w-lg">
        <div className="flex flex-wrap -mx-3 mb-6">
          <div className="w-full md:w-2/2 px-3 mb-6 md:mb-0">
            <label
              className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              htmlFor="grid-first-name"
            >
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
              <p className="text-red-500 text-xs italic">
                Debes introducir un correo electronico.
              </p>
            )}
          </div>
        </div>
        <div className="flex flex-wrap -mx-3 mb-6">
          <div className="w-full px-3">
            <label
              className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              htmlFor="grid-password"
            >
              Contraseña
            </label>

            <input
              className=" appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              name="password"
              value={formData.password}
              onChange={handleChange}
              type="password"
              placeholder="******************"
            />
            {(formData.password === null || formData.password.length === 0) && (
              <p className="text-red-500 text-xs italic">
                Debes introducir una contraseña.
              </p>
            )}
          </div>
        </div>
        <div className="flex flex-wrap -mx-3 mb-3">
          <div className="w-full px-3">
            <button
              className=" font-light appearance-none block w-full transition-all bg-blue-500 bold text-black border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-blue-600 hover:bg-blue-700 focus:border-gray-500"
              id="grid-password"
              name="password"
              type="submit"
              value={formData.password}
              onChange={handleChange}
              placeholder="*************"
            >
              Iniciar Sesión
            </button>
          </div>
        </div>
        <div className=" flex flex-wrap">
          <a className=" underline" href="#!">
            ¿Olvidaste tu contraseña?
          </a>
        </div>
      </form>

      <div className="my-4 flex items-center before:mt-0.5 before:flex-1 before:border-t before:border-neutral-300 after:mt-0.5 after:flex-1 after:border-t after:border-neutral-300">
        <p className="mx-4 mb-0 text-center font-semibold dark:text-neutral-200">
          O
        </p>
      </div>

      <div className=" flex  flex-wrap text-center gap-5">
        <button
          type="button"
          className="py-3 rounded-none  flex justify-center gap-2 items-center bg-blue-500 focus:ring-offset-gray-200 text-white w-full transition ease-in duration-200 text-center text-base font-semibold shadow-md focus:outline-none focus:ring-2 focus:ring-offset-2 "
        >
          <Google className=""></Google>
          <p className=" font-extralight">Iniciar Sesión con Google</p>
        </button>
        <button
          type="button"
          className="py-3 rounded-none flex justify-center gap-2 items-center bg-black focus:ring-offset-gray-200 text-white w-full transition ease-in duration-200 text-center text-base font-semibold shadow-md focus:outline-none focus:ring-2 focus:ring-offset-2 "
        >
          <GitHub className=""></GitHub>
          <p className=" font-extralight text-center">
            Iniciar Sesión con GitHub
          </p>
        </button>
      </div>
    </div>
  );
}
