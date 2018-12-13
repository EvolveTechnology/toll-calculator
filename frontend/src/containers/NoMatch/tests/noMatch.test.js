import React from "react";
import { shallow } from "enzyme";
import NoMatch from "..";

describe("Not Found, no Match", () => {
  const app = shallow(<NoMatch />);

  it("renders", () => {
    expect(app).toBeDefined();
  });

  it("shows 404 title", () => {
    expect(app.childAt(0).text()).toEqual("404");
  });

  it("shows 404 message", () => {
    expect(app.childAt(1).text()).toEqual("That Page was not found.");
  });
});
