const TollCalculator = require("../toll-calculator");
const Vehicle = require("../vehicle");
let tollCalculator;

describe("Calculate toll amount for multiple dates", () => {
  beforeAll(() => {
    tollCalculator = new TollCalculator();
  });


  it("Gets the toll amount with unsorted dates", () => {
    const toll = tollCalculator.getTollFee(new Vehicle("Car"), [
      new Date("2013-01-11T16:45:00"),
      new Date("2013-01-11T15:07:00"),
    ]);
    expect(toll).toBe(31);
  });



  it("Gets the toll amount with mixed dates", () => {
    const toll = tollCalculator.getTollFee(new Vehicle("Car"), [
      new Date("2013-01-11T16:45:00"),
      new Date("2013-01-11T15:45:00"),
      new Date("2013-01-11T15:07:00"),
    ]);
    expect(toll).toBe(36);
  });



  it("Gets the toll amount for dates more than an hour", () => {
    const toll = tollCalculator.getTollFee(new Vehicle("Car"), [
      new Date("2013-01-11T15:07:00"),
      new Date("2013-01-11T16:45:00"),
    ]);
    expect(toll).toBe(31);
  });

  it("Gets toll amount for dates with multiple fees within an hour which gets the largest one ", () => {
    const toll = tollCalculator.getTollFee(new Vehicle("Car"), [
      new Date("2013-01-11T15:07:00"),
      new Date("2013-01-11T15:45:00"),
    ]);
    expect(toll).toBe(18);
  });



  it("Gets toll amount for dates with multiple fees of within an hour and more than an hour", () => {
    const toll = tollCalculator.getTollFee(new Vehicle("Car"), [
      new Date("2013-01-11T15:07:00"),
      new Date("2013-01-11T15:45:00"),
      new Date("2013-01-11T16:25:00")
    ]);
    expect(toll).toBe(36);
  });

});
