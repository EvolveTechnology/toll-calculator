import React from "react";
import error from "../../assets/error.png";
import empty from "../../assets/empty.png";
import offline from "../../assets/offline.png";
import "./placeholder.css";

const placeholders = { error, empty, offline };
const alts = {
  error: "Network Error.",
  empty: "Zero Matches.",
  offline: "You are offline."
};

export default ({ placeholder }) => (
  <div className="placeholder-container">
    <img src={placeholders[placeholder]} alt={alts[placeholder]} width="64px" />
    <span>{alts[placeholder]}</span>
  </div>
);
