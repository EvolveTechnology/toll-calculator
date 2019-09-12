import { Time } from "./Time";

describe("Time", () => {
  describe("fromDate", () => {
    it("can construct from date", () => {
      const time = Time.fromDate(new Date("2019-09-11 10:42"));
      expect(time.hour).toBe(10);
      expect(time.minute).toBe(42);
    });
  });
  describe("isAfter", () => {
    it.each<[string, string, boolean]>([
      ["10:50", "00:20", true],
      ["10:50", "00:59", true],
      ["10:50", "11:20", false],
      ["10:50", "10:51", false]
    ])("%p isAfter %p = %p", (time1, time2, result) => {
      const [hour1, minute1] = time1.split(":").map(a => parseInt(a));
      const [hour2, minute2] = time2.split(":").map(a => parseInt(a));
      expect(new Time(hour1, minute1).isAfter(new Time(hour2, minute2))).toBe(
        result
      );
    });
  });
  describe("diffInMinutes", () => {
    it.each<[string, string, number]>([
      ["10:30", "11:30", -60],
      ["11:30", "11:20", 10]
    ])("%p isAfter %p = %p", (time1, time2, result) => {
      const [hour1, minute1] = time1.split(":").map(a => parseInt(a));
      const [hour2, minute2] = time2.split(":").map(a => parseInt(a));
      expect(
        new Time(hour1, minute1).diffInMinutes(new Time(hour2, minute2))
      ).toBe(result);
    });
  });
});
