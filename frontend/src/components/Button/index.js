import React from "react";

import download from "../../assets/download.png";
import slideup from "../../assets/slideup.png";
import undo from "../../assets/undo.png";

const buttonTypes = {
  download,
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
