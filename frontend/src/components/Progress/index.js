import React from "react";
import Circle from "../Circle";
import "./progress.css";

export const Progress = ({ value = 0, label, unit = "SEK", ...props }) => {
  return (
    <div className="progress-with-label">
      <div className="progress-wrapper">
        <Circle {...props} />
        <div className="fee">
          {props.freeDay ? (
            props.children
          ) : (
            <div className="fee-content">
              {value} {unit}
            </div>
          )}
        </div>
      </div>
      <div className="label">
        <span>{label}</span>
      </div>
    </div>
  );
};

export default Progress;
