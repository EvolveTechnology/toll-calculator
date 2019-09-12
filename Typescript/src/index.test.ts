import { getTollFee } from ".";
import { Vehicle } from "Vehicle";

describe("getTollFee", () => {
  describe("single day", () => {
    it.each<[Vehicle, string[], number]>([
      ["Car", [], 0],
      ["Car", ["00:30"], 0],
      ["Car", ["05:59"], 0],
      ["Car", ["19:30"], 0],
      ["Car", ["10:30"], 8],
      ["Car", ["07:30", "10:30"], 18 + 8],
      ["Car", ["07:30", "10:30", "12:30", "16:20", "17:20", "18:20"], 60], // Maximum fee
      ["Car", ["07:30", "08:10"], 18], // Two times, same hour period
      ["Car", ["06:20", "06:50", "07:10"], 18], // Three times, same hour period
      ["Car", ["06:20", "06:50", "07:20"], 18 + 13], // Get highest fee for first two in same hour period
      ["Diplomat", ["10:30"], 0],
      ["Motorbike", ["10:30"], 0],
      ["Emergency", ["10:30"], 0],
      ["Foreign", ["10:30"], 0],
      ["Military", ["10:30"], 0],
      ["Tractor", ["10:30"], 0]
    ])("%p on weekday %p should return %p", (vehicle, times, expectedFee) => {
      const fee = getTollFee(
        times.map(time => new Date(`2019-09-11 ${time}`)),
        vehicle
      );
      expect(fee).toBe(expectedFee);
    });
  });
  describe("on holiday", () => {
    it.each<[Vehicle, string[], number]>([
      ["Car", [], 0],
      ["Car", ["07:30", "10:30", "12:30", "16:20", "17:20", "18:20"], 0],
      ["Car", ["07:30", "08:10"], 0]
    ])("%p on weekday %p should return %p", (vehicle, times, expectedFee) => {
      const fee = getTollFee(
        times.map(time => new Date(`2019-12-25 ${time}`)),
        vehicle
      );
      expect(fee).toBe(expectedFee);
    });
  });
  describe("multiple days", () => {
    it.each<[Vehicle, string[], string[], number]>([
      ["Car", [], [], 0],
      [
        "Car",
        ["07:30", "08:50", "17:00"],
        ["07:20", "17:00"],
        18 + 8 + 13 + 18 + 13
      ]
    ])(
      "%p on weekday %p should return %p",
      (vehicle, firstDayTimes, secondDayTimes, expectedFee) => {
        const fee = getTollFee(
          [
            ...firstDayTimes.map(time => new Date(`2019-09-11 ${time}`)),
            ...secondDayTimes.map(time => new Date(`2019-09-12 ${time}`))
          ],
          vehicle
        );
        expect(fee).toBe(expectedFee);
      }
    );
  });
});
