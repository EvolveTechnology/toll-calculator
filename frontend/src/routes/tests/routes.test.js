import React from "react";
import { mount } from "enzyme";
import { MemoryRouter } from "react-router-dom";
import Routes from "..";

import Home from "../../containers/Home";
import Dashboard from "../../containers/Dashboard";
import Nav from "../../containers/Nav";
import NoMatch from "../../containers/NoMatch";

describe("It, renders the adequate component for every route", () => {
  const home = mount(
    <MemoryRouter initialEntries={["/"]}>
      <Routes />
    </MemoryRouter>
  );

  const dashboard = mount(
    <MemoryRouter initialEntries={["/dashboard"]}>
      <Routes />
    </MemoryRouter>
  );

  const noMatch = mount(
    <MemoryRouter initialEntries={["/30303"]}>
      <Routes />
    </MemoryRouter>
  );
  it("renders home", () => {
    expect(home.find(Home)).toHaveLength(1);
    expect(home.find(Nav)).toHaveLength(1);
    expect(home.find(Dashboard)).toHaveLength(0);
    expect(home.find(NoMatch)).toHaveLength(0);
  });
  it("renders dashboard", () => {
    expect(dashboard.find(Home)).toHaveLength(0);
    expect(home.find(Nav)).toHaveLength(1);
    expect(dashboard.find(Dashboard)).toHaveLength(1);
    expect(dashboard.find(NoMatch)).toHaveLength(0);
  });
  it("renders 404", () => {
    expect(noMatch.find(Home)).toHaveLength(0);
    expect(home.find(Nav)).toHaveLength(1);
    expect(noMatch.find(Dashboard)).toHaveLength(0);
    expect(noMatch.find(NoMatch)).toHaveLength(1);
  });
});
