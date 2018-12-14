import React from "react";
import { shallow } from "enzyme";
import Results from "..";
import Weekend from "../../Weekend";
import TollFreeVehicle from "../../TollFreeVehicle";
import Holiday from "../../Holiday";

describe("Results for tollFree Vehicle", () => {
  const props = {
    results: [],
    isTollFree: true,
    type: "Diplomat",
    regNum: "ABC-123",
    allTimeTotalFee: 0
  };

  const results = shallow(<Results {...props} />);

  it("loads appropiate results for the given vehicle", () => {
    expect(results).toBeDefined();
    expect(results.find(TollFreeVehicle)).toHaveLength(1);
  });
});

describe("Results for holidays", () => {
  const props = {
    results: [{ day: "a", isHoliday: true, isSunday: false, isSaturday: true }],
    isTollFree: false,
    type: "Truck",
    regNum: "ABC-123",
    allTimeTotalFee: 0
  };

  const results = shallow(<Results {...props} />);

  it("loads appropiate holidays for the given vehicle, without the weekend", () => {
    expect(results).toBeDefined();
    expect(results.find(Holiday)).toHaveLength(1);
    expect(results.find(Weekend)).toHaveLength(0);
  });
});

describe("Results for weekends", () => {
  const props = {
    results: [
      { day: "a", isHoliday: false, isSunday: false, isSaturday: true },
      { day: "b", isHoliday: false, isSunday: true, isSaturday: false }
    ],
    isTollFree: false,
    type: "Truck",
    regNum: "ABC-123",
    allTimeTotalFee: 0
  };

  const results = shallow(<Results {...props} />);

  it("loads appropiate weekends for the given vehicle", () => {
    expect(results).toBeDefined();
    expect(results.find(Weekend)).toHaveLength(2);
  });
});
