import React from "react";
import { capitalize } from "../../utils";

export default ({ change, title, options }) => (
  <div>
    <label htmlFor="type-select">{capitalize(title)}</label>
    <select id="type-select" className="custom-select" onChange={change}>
      {options.map(type => (
        <option key={type} value={type}>
          {type}
        </option>
      ))}
    </select>
  </div>
);
