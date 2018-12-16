import React from "react";
import { shallow } from "enzyme";
import Fabs from "..";
import { activateButton } from "../helpers";
import Button from "../../Button";

describe("", () => {
  const scrollMock = jest.fn();
  const addEvent = jest.fn();
  const removeEvent = jest.fn();

  const scrollToMock = function(a, b) {
    return scrollMock(a, b);
  };

  Object.defineProperty(window, "scrollTo", {
    value: scrollToMock
  });

  const addEventListenerMock = function(evt, callback) {
    return addEvent(evt, callback);
  };

  Object.defineProperty(window, "addEventListener", {
    value: addEventListenerMock
  });

  const removeEventListenerMock = function(evt, callback) {
    return removeEvent(evt, callback);
  };

  Object.defineProperty(window, "removeEventListener", {
    value: removeEventListenerMock
  });

  const fabs = shallow(<Fabs />);

  fabs.setState({ show: true });
  fabs.update();

  it("renders", () => {
    expect(fabs).toBeDefined();
    expect(fabs.find(Button)).toHaveLength(1);
  });

  it("scrolls back to top", () => {
    const distance = 1;
    document.body.scrollTop = distance;
    const afterStep = distance - distance / 8;
    fabs.find(Button).simulate("click");
    expect(scrollMock).toHaveBeenCalledWith(0, afterStep);
  });

  it("removes listener on unmount", () => {
    fabs.instance().componentWillUnmount();
    expect(removeEvent).toHaveBeenCalled();
  });
});

describe("helpers", () => {
  const setState = jest.fn();
  const ctx0 = {
    state: { show: false },
    setState
  };

  const ctx1 = {
    state: { show: true },
    setState
  };

  const bound0 = activateButton.bind(ctx0);
  const bound1 = activateButton.bind(ctx1);

  it("activates button", () => {
    Object.defineProperty(window, "scrollY", {
      value: 102,
      configurable: true,
      writable: true
    });
    bound0();
    expect(ctx0.setState).toHaveBeenCalledWith({ show: !ctx0.state.show });
  });

  it("deactivates button", () => {
    Object.defineProperty(window, "scrollY", {
      value: 99,
      configurable: true,
      writable: true
    });
    bound1(ctx1);
    expect(ctx1.setState).toHaveBeenCalledWith({ show: !ctx1.state.show });
  });
});
