import React from "react";
import { NavLink } from "react-router-dom";

export default () => (
  <nav className="flex-nav">
    <NavLink className="nav-link" to="/">
      Home
    </NavLink>
    <NavLink className="nav-link" to="/dashboard">
      Dashboard
    </NavLink>
  </nav>
);
