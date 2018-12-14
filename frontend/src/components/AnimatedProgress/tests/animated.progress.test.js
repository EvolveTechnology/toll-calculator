import React from "react";
import { shallow } from "enzyme";
import AnimatedProgress from "..";

describe("AnimatedProgress", () => {
  jest.useFakeTimers();

  const animatedProgress = shallow(<AnimatedProgress value={1} />);
  it("renders", () => {
    expect(animatedProgress.state("percentage")).toEqual(0);
    expect(animatedProgress).toBeDefined();
    expect(setInterval).toHaveBeenCalled();
  });

  it("ticks", () => {
    jest.runAllTimers();
    expect(animatedProgress.state("percentage")).toEqual(2);
  });

  it("clearsInterval", () => {
    animatedProgress.instance().componentWillUnmount();
    expect(clearInterval).toHaveBeenCalled();
  });
});
