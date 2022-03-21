const TollCalculator = require("../tollCalculator");
const Vehicle = require("../vehicle");
let tollCalculator;

describe("Getting the toll amount for date and time", () => {
  beforeAll(() => {
    tollCalculator = new TollCalculator();
  });

  it("Toll fee between 0:00 - 5:59 = 0", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-01-11T02:07:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T04:07:00"),
      new Vehicle("Bike")
    );
    const scooterToll = tollCalculator.getTollFee(
      new Date("2013-01-11T00:00:00"),
      new Vehicle("Scooter")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
    expect(scooterToll).toBe(0);
  });

  it("Toll fee between 6:00 - 6:29 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T06:07:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(8);
  });

  it("Toll fee between 6:30 - 6:59 = 13", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T06:30:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(13);
  });

  it("Toll fee between 7:00 - 7:59 = 18", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T07:22:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(18);
  });

  it("Toll fee between 8:00 - 7:29 = 13", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T08:11:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(13);
  });

  it("Toll fee between 8:30 - 8:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T08:34:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(8);
  });

  it("Toll fee between 9:30 - 9:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T09:34:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T09:05:00"),
      new Vehicle("Bike")
    );
    expect(toll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 10:30 - 10:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T10:34:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T10:05:00"),
      new Vehicle("Bike")
    );
    expect(toll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 11:30 - 11:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T11:34:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T11:05:00"),
      new Vehicle("Bike")
    );
    expect(toll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 12:30 - 12:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T12:34:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T12:05:00"),
      new Vehicle("Bike")
    );
    expect(toll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 13:30 - 13:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T13:34:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T13:05:00"),
      new Vehicle("Bike")
    );
    expect(toll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 14:30 - 14:59 = 8", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T14:34:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T14:05:00"),
      new Vehicle("Bike")
    );
    expect(toll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 15:00 - 15:29 = 13", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:05:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(13);
  });

  it("Toll fee between 15:30 - 16:59 = 18", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T16:22:00"),
      new Vehicle("Bike")
    );
    expect(carToll).toBe(18);
    expect(bikeToll).toBe(18);
  });

  it("Toll fee between 17:00 - 17:59 = 13", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-01-11T17:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T17:22:00"),
      new Vehicle("Bike")
    );
    expect(carToll).toBe(13);
    expect(bikeToll).toBe(13);
  });

  it("Toll fee between 18:00 - 18:29 = 8", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-01-11T18:29:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T18:30:00"),
      new Vehicle("Bike")
    );
    expect(carToll).toBe(8);
    expect(bikeToll).toBe(0);
  });

  it("Toll fee between 19:00 - 23:59 = 0", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-01-11T19:29:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-01-11T20:55:00"),
      new Vehicle("Bike")
    );
    const scooterToll = tollCalculator.getTollFee(
      new Date("2013-01-11T23:55:00"),
      new Vehicle("Bike")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
    expect(scooterToll).toBe(0);
  });
});
