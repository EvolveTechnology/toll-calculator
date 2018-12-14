import React from "react";
import { shallow } from "enzyme";
import TollFreeVehicle from "..";

describe("TollFreeVehicle", () => {
  it("renders for Saturday", () => {
    const tollFreeVehicle = shallow(<TollFreeVehicle />);
    expect(tollFreeVehicle).toBeDefined();
    expect(tollFreeVehicle.find("img")).toHaveLength(1);
    expect(tollFreeVehicle.find("span").text()).toEqual(
      "This vehicle is toll free!"
    );
  });
});
