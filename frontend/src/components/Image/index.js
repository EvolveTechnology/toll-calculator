import React from "react";
import { safeClick } from "../../utils";

const isClickable = onClick =>
  typeof onClick === "function" ? "clickable" : "";

export default ({ onClick, payload, src, alt }) => (
  <div className={isClickable(onClick)} onClick={safeClick(onClick, payload)}>
    <img src={src} alt={alt} width="64px" />
  </div>
);
