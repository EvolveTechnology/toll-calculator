import React from "react";
import { shallow } from "enzyme";
import withResultData from "..";
import vehicles from "./mock";

// vehicles is an array of one element;

describe("withResultData", () => {
  const Target = ({ ...props }) => <div {...props} />;

  const args = ["None", vehicles];

  const element = shallow(withResultData(Target, ...args));

  it("injects data to the target", () => {
    const passedProps = element.props();
    expect(passedProps).toHaveProperty("id");
    expect(passedProps).toHaveProperty("allTimeTotalFee");
    expect(passedProps).toHaveProperty("isTollFree");
    expect(passedProps).not.toHaveProperty("type");
  });
});
