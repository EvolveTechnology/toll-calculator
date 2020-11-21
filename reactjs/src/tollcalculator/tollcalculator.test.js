import * as TollCalculator from "./tollcalculator";

describe("get total fee for a vehicle's passages a tollable date", () => {
  test("get total fee for a date", () => {
    const vehicle = {
      type: "car",
      passages: {
        "2020-11-18": ["06:20:26", "07:06:10", "15:06:10", "15:30:11"],
      },
    };
    const received = TollCalculator.getTotalFeeForDate(vehicle, "2020-11-18");
    expect(received).toBe(36);
  });
  test("maximum fee for one day is 60 SEK", () => {
    const vehicle = {
      type: "truck",
      passages: {
        "2020-11-18": [
          "06:00:00",
          "07:00:00",
          "08:00:00",
          "09:00:00",
          "10:00:00",
          "11:00:00",
          "12:00:00",
        ],
      },
    };
    const received = TollCalculator.getTotalFeeForDate(vehicle, "2020-11-18");
    expect(received).not.toBe(71); // Total fee if no daily max fee
    expect(received).toBe(60);
  });
});

test("a vehicle should only be charged once an hour", () => {
  const vehicle = {
    type: "car",
    passages: {
      "2020-11-18": ["06:00:00", "06:59:59", "07:00:00"],
    },
  };

  const received = TollCalculator.getTotalFeeForDate(vehicle, "2020-11-18");
  expect(received).toBe(31);
});

test("get highest fee for passages within an hour", () => {
  const passages = ["14:59:59", "15:00:00", "15:59:58"];
  const received = TollCalculator.getHighestFeeForHourSlot(passages);
  expect(received).toBe(18);
});

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

test("get not charged passages for a date", () => {
  const received = TollCalculator.notChargedPassagesForDate(
    ["06:00:00", "07:00:00", "07:30:00", "07:59:59"],
    "2020-11-19"
  );
  expect(received).toEqual(expect.arrayContaining(["07:30:00", "07:59:59"]));
});
