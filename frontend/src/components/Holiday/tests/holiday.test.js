import React from "react";
import { shallow } from "enzyme";
import Holiday from "..";

describe("Holiday", () => {
  it("renders", () => {
    expect(shallow(<Holiday />)).toBeDefined();
  });
  it("is a React Element", () => {
    expect(React.isValidElement(Holiday)).toBeDefined();
  });
});
