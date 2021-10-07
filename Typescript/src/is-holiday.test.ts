import isHoliday from './is-holiday'

const holidays2022 = [
  new Date(2022, 0, 1),
  new Date(2022, 0, 6),
  new Date(2022, 3, 15),
  new Date(2022, 3, 17),
  new Date(2022, 3, 18),
  new Date(2022, 4, 1),
  new Date(2022, 4, 26),
  new Date(2022, 5, 5),
  new Date(2022, 5, 6),
  new Date(2022, 5, 25),
  new Date(2022, 10, 5),
  new Date(2022, 11, 25),
  new Date(2022, 11, 26),
]

describe('isHoliday', () => {
  test.each(holidays2022)('should return true for each holiday', date =>
    expect(isHoliday(date)).toEqual(true),
  )
})
