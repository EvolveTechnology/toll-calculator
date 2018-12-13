import React from "react";

import total from "../../assets/total.png";
import "./type.css";

export const Type = ({
  src,
  type,
  regNum,
  unit = "SEK",
  className = "",
  totalFee
}) => (
  <div className={`type lead ${className}`}>
    <div>
      <img src={src} alt={type} width="64px" />
    </div>
    <div>
      <span>{regNum}</span>
      <span>Type: {type}</span>
      <span>
        Total: {totalFee} {unit}
        <img src={total} alt="total" width="16px" />
      </span>
    </div>
  </div>
);

export default Type;
