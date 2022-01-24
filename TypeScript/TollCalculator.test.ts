import { Vehicle } from "./helpers";
import TollCalculator from "./TollCalculator";

const tollCalculator = new TollCalculator({ useLocalTime: false });

// In 1995, 10/12 was a Saturday, 11/12 was a Sunday and 12/12 was a Monday.
// Months are 0 indexed when initializing date objects.
describe("TollCalculator", () => {
  test("Saturdays are toll free days", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 10))
    );
    expect(fee).toBe(0);
  });

  test("Sundays are toll free days", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 11))
    );
    expect(fee).toBe(0);
  });

  test("Cars are not toll free", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 6, 40))
    );
    expect(fee).toBe(13);
  });

  test("Motorbikes are toll free", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Motorbike,
      new Date(Date.UTC(1995, 11, 12, 6, 40))
    );
    expect(fee).toBe(0);
  });

  test("The fee is 8 at 6 hours 20 minutes", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 6, 20))
    );
    expect(fee).toBe(8);
  });

  test("The fee is 13 at 6 hours and 40 minutes", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 6, 40))
    );
    expect(fee).toBe(13);
  });

  test("The fee is 18 at 7 hours and 40 minutes", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 7, 40))
    );
    expect(fee).toBe(18);
  });

  test("The fee is 8 at 10 hours and 40 minutes", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 10, 40))
    );
    expect(fee).toBe(8);
  });

  test("The fee is 18 at 15 hours and 59 minutes", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 15, 59))
    );
    expect(fee).toBe(18);
  });

  test("The fee is 0 before 6 and after 18", () => {
    const fee1 = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 3, 1))
    );
    const fee2 = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(1995, 11, 12, 20, 1))
    );
    expect(fee1).toBe(0);
    expect(fee2).toBe(0);
  });

  test("January 1 2013 is a toll free day", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(2013, 0, 1, 12, 1))
    );
    expect(fee).toBe(0);
  });

  test("The whole of July 2013 is toll free", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(2013, 6, 10, 15, 1))
    );
    expect(fee).toBe(0);
  });

  test("Christmas eve is toll free 2013", () => {
    const fee = tollCalculator.getTollFee(
      Vehicle.Car,
      new Date(Date.UTC(2013, 11, 24, 15, 1))
    );
    expect(fee).toBe(0);
  });

  test("It handles multiple billings in a day", () => {
    const fee = tollCalculator.getTotalTollFee(Vehicle.Car, [
      new Date(Date.UTC(1995, 11, 12, 8, 40)),
      new Date(Date.UTC(1995, 11, 12, 9, 45)),
    ]);

    expect(fee).toBe(16);
  });

  test("It handles passing billings in unsorted order", () => {
    const fee = tollCalculator.getTotalTollFee(Vehicle.Car, [
      new Date(Date.UTC(1995, 11, 12, 9, 45)),
      new Date(Date.UTC(1995, 11, 12, 8, 40)),
    ]);

    expect(fee).toBe(16);
  });

  test("In case there are multiple billings in one hour, it only bills the largest one", () => {
    const fee = tollCalculator.getTotalTollFee(Vehicle.Car, [
      new Date(Date.UTC(1995, 11, 12, 8, 20)),
      new Date(Date.UTC(1995, 11, 12, 8, 40)),
    ]);

    expect(fee).toBe(13);
  });

  test("Max fee is 60", () => {
    const fee = tollCalculator.getTotalTollFee(Vehicle.Car, [
      new Date(Date.UTC(1995, 11, 12, 8, 40)),
      new Date(Date.UTC(1995, 11, 12, 9, 41)),
      new Date(Date.UTC(1995, 11, 12, 10, 42)),
      new Date(Date.UTC(1995, 11, 12, 11, 43)),
      new Date(Date.UTC(1995, 11, 12, 12, 44)),
      new Date(Date.UTC(1995, 11, 12, 13, 45)),
      new Date(Date.UTC(1995, 11, 12, 15, 46)),
      new Date(Date.UTC(1995, 11, 12, 16, 47)),
      new Date(Date.UTC(1995, 11, 12, 17, 48)),
    ]);

    expect(fee).toBe(60);
  });
});
