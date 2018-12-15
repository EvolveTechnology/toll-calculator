import React from "react";

import { mount } from "enzyme";
import Offline from "..";
import Placeholder from "../../../components/Placeholder";

describe("Offline HoC", () => {
  const nativeAdd = window.addEventListener;
  window.addEventListener = jest.fn((evt, cb) => nativeAdd(evt, cb));

  const nativeRemove = window.removeEventListener;
  window.removeEventListener = jest.fn((evt, cb) => nativeRemove(evt, cb));

  const onLineMock = (function(a) {
    return { onLine: true };
  })();

  Object.defineProperty(window, "navigator", {
    value: onLineMock,
    configurable: true,
    writable: true
  });

  const offLineEvent = new Event("offline");
  const onLineEvent = new Event("online");
  const Child = ({ ...props }) => <div {...props} />;

  const offline = mount(
    <Offline>
      <Child />
    </Offline>
  );

  it("adds listeners on Mount", () => {
    expect(window.addEventListener).toHaveBeenCalled();
  });

  it("changes state to offline", () => {
    window.dispatchEvent(offLineEvent);
    expect(offline.state("online")).toEqual(false);
  });

  it("has no children, only placeholder, while offline", () => {
    offline.update();
    expect(offline.find(Placeholder)).toHaveLength(1);
    expect(offline.find(Placeholder).prop("placeholder")).toEqual("offline");
  });

  it("changes state back to online", () => {
    window.dispatchEvent(onLineEvent);
    expect(offline.state("online")).toEqual(true);
  });

  it("has children, while online", () => {
    offline.update();
    expect(offline.find(Child)).toHaveLength(1);
  });

  it("removes listeners on unMount", () => {
    offline.instance().componentWillUnmount();
    expect(window.removeEventListener).toHaveBeenCalled();
  });
});
