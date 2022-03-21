const TollCalculator = require("../tollCalculator");
const Vehicle = require("../vehicle");
let tollCalculator;

describe("Toll free vehicles", () => {
  beforeAll(() => {
    tollCalculator = new TollCalculator();
  });

  it("Military vehicles are toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Military")
    );
    expect(toll).toBe(0);
  });

  it("Tractor vehicles are toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Tractor")
    );
    expect(toll).toBe(0);
  });

  it("Motorbike vehicles are toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Motorbike")
    );
    expect(toll).toBe(0);
  });

  it("Emergency vehicles are toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Emergency")
    );
    expect(toll).toBe(0);
  });

  it("Diplomat vehicles are toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Diplomat")
    );
    expect(toll).toBe(0);
  });

  it("Foreign vehicles are toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Foreign")
    );
    expect(toll).toBe(0);
  });

  it("Van is not toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("Van")
    );
    expect(toll).toBe(18);
  });
  it("Car is not toll free", () => {
    const toll = tollCalculator.getTollFee(
      new Date("2013-01-11T15:48:00"),
      new Vehicle("car")
    );
    expect(toll).toBe(18);
  });
});