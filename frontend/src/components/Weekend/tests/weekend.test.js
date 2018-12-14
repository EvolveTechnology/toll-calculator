import React from "react";
import { shallow } from "enzyme";
import Weekend from "..";

describe("Weekend", () => {
  it("renders for Saturday", () => {
    const weekend = shallow(<Weekend isSaturday={true} isSunday={false} />);
    expect(weekend).toBeDefined();
    expect(weekend.find("span").text()).toEqual("Weekend");
  });
  it("renders for Sunday", () => {
    const weekend = shallow(<Weekend isSaturday={false} isSunday={true} />);
    expect(weekend).toBeDefined();
    expect(weekend.find("span").text()).toEqual("Weekend");
  });
});
