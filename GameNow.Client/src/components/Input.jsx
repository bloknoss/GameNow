import React from "react";

const Input = ({ value, label, name, placeholder, type, onChange }) => (
  <div className=" flex flex-col">
    {label && <label htmlFor="input-field">{label}</label>}
    <input
      type={type}
      value={value}
      name={name}
      className="form-control"
      placeholder={placeholder}
      onChange={onChange}
    />
  </div>
);

export default Input;
