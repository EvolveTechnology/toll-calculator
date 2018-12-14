import React from "react";
import { shallow } from "enzyme";
import Spinner from "..";

describe("Spinner", () => {
  jest.useFakeTimers();

  const props = {
    show: false
  };

  const spinner = shallow(<Spinner {...props} />);

  it("starts by not showing", () => {
    expect(spinner.children()).toHaveLength(0);
  });

  it("mounts the spinner", () => {
    spinner.setProps({ show: true });
    expect(spinner.children()).toHaveLength(1);
  });

  it("unmounts the spinner", () => {
    spinner.setProps({ show: false });
    jest.runAllTimers();
    expect(clearTimeout).toHaveBeenCalled();
    expect(spinner.children()).toHaveLength(0);
  });

  it("clears the timer", () => {
    spinner.instance().componentWillUnmount();
    expect(clearTimeout).toHaveBeenCalled();
  });
});
