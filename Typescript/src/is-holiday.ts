import { easter } from 'date-easter'
import { add, previousFriday, eachDayOfInterval, isSameDay } from 'date-fns'

/**
 * Get a the first specified weekday within a range of days
 *
 * @param {Date} startDate the start date of the range
 * @param {Date} endDate the end date of the range
 * @param {number} weekday a weekday number
 * @returns {Date} a date object
 */
function getWeekdayInRange(
  startDate: Date,
  endDate: Date,
  weekday: number,
): Date {
  const dateRange = eachDayOfInterval({ start: startDate, end: endDate })
  return dateRange.find(date => date.getDay() === weekday) as Date
}

/**
 * Get a list of swedish holiday dates based on year.
 * List from https://www.kalender.se/helgdagar, calculations from Wikipedia
 *
 * @param {number} year A year
 * @returns {Date[]} an array of swedish holiday dates
 */
function getHolidaysByYear(year: number): Date[] {
  const { month: easterMonth, day: easterDay } = easter(year)
  const easterDate = new Date(year, easterMonth - 1, easterDay)

  return [
    // New Years Eves
    new Date(year, 0, 1),
    // 13th Day of Christmas
    new Date(year, 0, 6),
    // Good Friday
    previousFriday(easterDate),
    // Easter Day
    easterDate,
    // Day after Easter Day
    add(easterDate, { days: 1 }),
    // May 1st
    new Date(year, 4, 1),
    // Ascension Day
    add(easterDate, { days: 39 }),
    // Pentecost Day (Always on a Sunday so might be redundant, but still a holiday :-)
    add(easterDate, { days: 49 }),
    // National Day of Sweden
    new Date(year, 5, 6),
    // Midsummer Day
    getWeekdayInRange(new Date(year, 5, 20), new Date(year, 5, 26), 6),
    // All Saints Day
    getWeekdayInRange(new Date(year, 9, 31), new Date(year, 10, 6), 6),
    // Christmas Day
    new Date(year, 11, 25),
    // Boxing Day
    new Date(year, 11, 26),
  ]
}

/**
 * Check if provided date is a swedish holiday
 *
 * @param {Date} date a date
 * @returns {boolean} true if provided date is a swedish holiday
 */
function isHoliday(date: Date): boolean {
  const year = date.getFullYear()
  const holidays = getHolidaysByYear(year)

  return holidays.some(holiday => isSameDay(holiday, date))
}

export default isHoliday
