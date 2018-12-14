import React from "react";
import Circle from "../Circle";
import "./progress.css";

export const Progress = ({
  value,
  label,
  unit = "SEK",
  freeDay,
  children,
  ...props
}) => {
  return (
    <div className="progress-with-label">
      <div className="progress-wrapper">
        <Circle {...props} />
        <div className="fee">
          {freeDay ? (
            children
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
