import React from "react";
import { shallow } from "enzyme";
import Nav from "..";

describe("Nav", () => {
  const nav = shallow(<Nav />);
  it("renders", () => {
    expect(nav).toBeDefined();
    expect(nav.children()).toHaveLength(2);
  });
});
