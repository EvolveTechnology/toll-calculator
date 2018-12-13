import React from "react";
import Type from "../Type";
import typeIconSelector from "../Type/helpers";

export default ({ vehicles }) => (
  <div className="vehicle-list">
    {vehicles.map(({ id, type, ...rest }) => (
      <Type
        key={id}
        src={typeIconSelector(type)}
        type={type}
        {...rest}
        className="for-vehicle-list"
      />
    ))}
  </div>
);
