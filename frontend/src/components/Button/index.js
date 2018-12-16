import React from "react";

import load from "../../assets/load.png";
import slideup from "../../assets/slideup.png";
import undo from "../../assets/undo.png";

const buttonTypes = {
  load,
  slideup,
  undo
};

export default ({ type, onClick }) => (
  <img
    className="clickable"
    onClick={onClick}
    alt={type}
    src={buttonTypes[type]}
    width="64px"
  />
);
