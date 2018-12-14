import React from "react";
import { capitalize } from "../../utils";

export default ({ change, title, options, disabled }) => (
  <div>
    <label htmlFor="type-select">{capitalize(title)}</label>
    <select
      id="type-select"
      className="custom-select"
      onChange={change}
      disabled={disabled}
    >
      {options.map(type => (
        <option key={type} value={type}>
          {type}
        </option>
      ))}
    </select>
  </div>
);
