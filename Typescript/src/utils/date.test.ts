import { isWeekend, isHoliday } from "./date";

describe("date", () => {
  describe("isWeekend", () => {
    it.each<[string, boolean]>([
      ["2019-09-07", true], // last saturday
      ["2020-09-12", true], // next year saturday
      ["2019-08-11", true], // sunday
      ["2019-08-26", false], // monday
      ["2019-08-27", false], // tuesday
      ["2019-08-28", false], // wednesday
      ["2019-08-29", false], // thursday
      ["2019-09-13", false] // friday
    ])("%p isWeekend = %p", (date, expectedResult) => {
      expect(isWeekend(new Date(date))).toBe(expectedResult);
    });
  });
  describe("isHoliday", () => {
    it.each<[string, boolean]>([
      ["2020-01-01", true], // Nyårsdagen 2020
      ["2020-01-06", true], // Trettondedag jul 2020
      ["2020-04-10", true], // Långfredagen 2020
      ["2020-04-12", true], // Påskdagen 2020
      ["2020-04-13", true], // Annandag påsk 2020
      ["2020-05-01", true], // Första maj 2020
      ["2020-05-21", true], // Kristi himmelfärdsdag 2020
      ["2020-05-31", true], // Pingstdagen 2020
      ["2020-06-06", true], // Sveriges nationaldag 2020
      ["2020-06-20", true], // Midsommar 2020
      ["2020-10-31", true], // Alla helgons dag 2020
      ["2020-12-25", true], // Juldagen 2020
      ["2020-12-26", true], // Annandag jul 2020
      ["2017-01-01", true], // Nyårsdagen 2017
      ["2017-01-06", true], // Trettondedag 2017
      ["2017-04-14", true], // Långfredagen 2017
      ["2017-04-16", true], // Påskdagen 2017
      ["2017-04-17", true], // Annandag påsk 2017
      ["2017-05-01", true], // Första maj 2017
      ["2017-05-25", true], // Kristi himmelfärdsdag 2017
      ["2017-06-04", true], // Pingstdagen 2017
      ["2017-06-06", true], // Sveriges nationaldag 2017
      ["2017-06-24", true], // Midsommar 2017
      ["2017-11-04", true], // Alla helgons dag 2017
      ["2017-12-25", true], // Juldagen 2017
      ["2017-12-26", true], // Annandag jul 2017
      ["2019-09-07", false] // last saturday
    ])("%p isHoliday = %p", (date, expectedResult) => {
      expect(isHoliday(new Date(date))).toBe(expectedResult);
    });
  });
});
