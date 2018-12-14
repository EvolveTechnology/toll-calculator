import React from "react";
import { NavLink } from "react-router-dom";

const activeStyle = {
  color: "white"
};

export default () => (
  <nav className="flex-nav">
    <NavLink exact className="nav-link" to="/" activeStyle={activeStyle}>
      Home
    </NavLink>
    <NavLink className="nav-link" to="/dashboard" activeStyle={activeStyle}>
      Dashboard
    </NavLink>
  </nav>
);
