import React from "react";
import Image from "../Image";
import total from "../../assets/total.png";
import "./type.css";

export const Type = ({
  src,
  type,
  regNum,
  clickAction,
  unit = "SEK",
  className = "",
  totalFee
}) => (
  <div className={`type lead ${className}`}>
    <Image onClick={clickAction} payload={regNum} src={src} alt={type} />
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
