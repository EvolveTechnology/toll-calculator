import { toll, vehicles } from "./helpers"

test("Fees will differ between 0 SEK and 18 SEK, depending on the time of day", () => {
  const tests = [["05:00", 0], ["08:00", 8], ["09:00", 13], ["12:00", 18]]

  tests.forEach(([time, expected]) => {
    const actual = toll.getFee(vehicles.DEFAULT, [
      new Date("2017-09-04 " + time),
    ])
    expect(actual).toEqual(expected)
  })
})

test("The maximum fee for one day is 60 SEK", () => {
  const dates = [
    "2017-09-01 09:00",
    "2017-09-01 10:00",
    "2017-09-01 11:00",
    "2017-09-01 12:00",
    "2017-09-01 13:00",
    "2017-09-04 13:00", // new day to test day splitting
  ].map(t => new Date(t))

  const actual = toll.getFee(vehicles.DEFAULT, dates)
  expect(actual).toEqual(78 /* 60 + 18*/)
})

test("A vehicle should only be charged once an hour", () => {
  const dates = ["08:00", "08:01", "9:00"].map(t => new Date("2017-08-30 " + t))

  const actual = toll.getFee(vehicles.DEFAULT, dates)
  expect(actual).toEqual(21)
})

test("Some vehicle types are fee-free", () => {
  const actual = toll.getFee(vehicles.FREE)

  expect(actual).toEqual(0)
})

test("Weekends and holidays are fee-free", () => {
  let actual

  // xmas
  actual = toll.getFee(vehicles.DEFAULT, [new Date("2015-12-24 15:00")])
  expect(actual).toEqual(0)

  // saturday
  actual = toll.getFee(vehicles.DEFAULT, [new Date("2017-09-03 15:00")])
  expect(actual).toEqual(0)
})
