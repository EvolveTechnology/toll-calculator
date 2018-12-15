import React from "react";

export default ({ text, onClick }) => (
  <button
    className="btn btn-primary"
    onClick={onClick}
    onMouseDown={e => e.preventDefault()}
  >
    {text}
  </button>
);
