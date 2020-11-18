import * as TollCalculator from "./tollcalculator";

test("get correct fee for time", () => {
  const timesWithExpectedFee = [
    ["05:59:59", 0],
    ["06:00:00", 8],
    ["06:30:00", 13],
    ["07:00:00", 18],
    ["07:59:59", 18],
    ["08:00:00", 13],
    ["08:30:00", 8],
    ["15:00:00", 13],
    ["15:30:00", 18],
    ["16:59:59", 18],
    ["17:00:00", 13],
    ["17:59:59", 13],
    ["18:00:00", 8],
    ["18:30:00", 0],
    ["23:59:59", 0],
    ["00:00:00", 0],
  ];

  timesWithExpectedFee.forEach(([time, expected]) => {
    const received = TollCalculator.getFeeForTime(time);
    expect(received).toBe(expected);
  });
});

test("tractor vehicle type is toll free", () => {
  const vehicle = { type: "tractor" };
  const received = TollCalculator.isTollFreeVehicle(vehicle);
  expect(received).toBe(true);
});

describe("weekends and holidays are toll free dates", () => {
  test("saturday is toll free", () => {
    const received = TollCalculator.isTollFreeDate("2020-11-14");
    expect(received).toBe(true);
  });
  test("national day is toll free", () => {
    const received = TollCalculator.isTollFreeDate("2020-06-06");
    expect(received).toBe(true);
  });
});
