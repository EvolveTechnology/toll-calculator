import React from "react";
import Type from "../Type";
import typeIconSelector from "../Type/helpers";

export default ({ vehicles, vehicleClickAction }) => (
  <div className="vehicle-list">
    {vehicles.map(({ id, type, ...rest }) => (
      <Type
        key={id}
        clickAction={vehicleClickAction}
        src={typeIconSelector(type)}
        type={type}
        {...rest}
        className="for-vehicle-list"
      />
    ))}
  </div>
);
