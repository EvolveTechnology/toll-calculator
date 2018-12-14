import React from "react";
import { mount } from "enzyme";
import { MemoryRouter } from "react-router-dom";
import Routes from "..";

import Landing from "../../containers/Landing";
import Dashboard from "../../containers/Dashboard";
import Nav from "../../containers/Nav";
import NoMatch from "../../containers/NoMatch";

describe("It, renders the adequate component for every route", () => {
  const landing = mount(
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
  it("renders landing", () => {
    expect(landing.find(Landing)).toHaveLength(1);
    expect(landing.find(Nav)).toHaveLength(1);
    expect(landing.find(Dashboard)).toHaveLength(0);
    expect(landing.find(NoMatch)).toHaveLength(0);
  });
  it("renders dashboard", () => {
    expect(dashboard.find(Landing)).toHaveLength(0);
    expect(landing.find(Nav)).toHaveLength(1);
    expect(dashboard.find(Dashboard)).toHaveLength(1);
    expect(dashboard.find(NoMatch)).toHaveLength(0);
  });
  it("renders 404", () => {
    expect(noMatch.find(Landing)).toHaveLength(0);
    expect(landing.find(Nav)).toHaveLength(1);
    expect(noMatch.find(Dashboard)).toHaveLength(0);
    expect(noMatch.find(NoMatch)).toHaveLength(1);
  });
});
