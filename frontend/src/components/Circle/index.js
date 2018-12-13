import React from "react";
import "./circle.css";

export const Circle = ({ percentage, size = 120 }) => {
  const center = size / 2;
  const strokeWidth = size * 0.1;
  const radius = size / 2 - strokeWidth / 2;
  const circumference = 2 * Math.PI * radius;
  const offset = ((100 - percentage) / 100) * circumference;

  const style = {
    strokeDashoffset: offset
  };

  return (
    <svg
      className="circle"
      width={size}
      height={size}
      viewBox={`0 0 ${size} ${size}`}
    >
      <circle
        className="circle-background"
        cx={center}
        cy={center}
        r={radius}
        strokeWidth={strokeWidth}
      />
      <circle
        className="circle-fill"
        style={style}
        cx={center}
        cy={center}
        r={radius}
        strokeWidth={strokeWidth}
        strokeDasharray={circumference}
      />
    </svg>
  );
};

export default Circle;
