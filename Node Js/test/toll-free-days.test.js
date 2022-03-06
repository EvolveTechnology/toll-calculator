const TollCalculator = require("../toll-calculator");
const Vehicle = require("../vehicle");
let tollCalculator;

describe("Toll free dates", () => {
  beforeAll(() => {
    tollCalculator = new TollCalculator();
  });

  it("New year day is toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-01T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(0);
  });

  it("February is not toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-02-01T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).not.toBe(0);
  });

  it("Mar 28th and 29th is toll free", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-03-29T00:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-03-29T15:48:00"),
      new Vehicle("Bike")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
  });

  it("Apr 1st and 30th is toll free", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-04-01T00:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-04-30T15:48:00"),
      new Vehicle("Bike")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
  });

  it("May 1st, 8th and 9th is toll free", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-05-01T00:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-05-08T15:48:00"),
      new Vehicle("Bike")
    );
    const scooterToll = tollCalculator.getTollFee(
      new Date("2013-05-09T18:48:00"),
      new Vehicle("Scooter")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
    expect(scooterToll).toBe(0);
  });

  it("June 1st, 8th and 9th is toll free", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-06-01T00:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-06-08T15:48:00"),
      new Vehicle("Bike")
    );
    const scooterToll = tollCalculator.getTollFee(
      new Date("2013-06-09T18:48:00"),
      new Vehicle("Scooter")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
    expect(scooterToll).toBe(0);
  });

  it("July month is toll free", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-07-01T00:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-07-15T15:48:00"),
      new Vehicle("Bike")
    );
    const scooterToll = tollCalculator.getTollFee(
      new Date("2013-07-29T15:48:00"),
      new Vehicle("Scooter")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
    expect(scooterToll).toBe(0);
  });

  it("August is not toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-08-22T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).not.toBe(0);
  });

  it("September is not toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-09-06T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).not.toBe(0);
  });

  it("October is not toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-10-10T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).not.toBe(0);
  });

  it("November 1st is toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-11-01T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(0);
  });

  it("Christmas holidays are toll free", () => {
    const carToll = tollCalculator.getTollFee(
      new Date("2013-12-25T15:48:00"),
      new Vehicle("Car")
    );
    const bikeToll = tollCalculator.getTollFee(
      new Date("2013-12-24T15:48:00"),
      new Vehicle("Bike")
    );
    const scooterToll = tollCalculator.getTollFee(
      new Date("2013-12-26T15:48:00"),
      new Vehicle("Scooter")
    );
    expect(carToll).toBe(0);
    expect(bikeToll).toBe(0);
    expect(scooterToll).toBe(0);
  });

  it("December 31st is toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-12-31T15:48:00"),
      new Vehicle("Car")
    );
    expect(toll).toBe(0);
  });
});
