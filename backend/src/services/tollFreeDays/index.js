import {
  head, partialRight, split, localTime, pipe,
} from '../../utils';

const toISO = date => date.toISOString();
const splitISO = partialRight(split)('T');

const calendarDate = pipe(
  localTime,
  toISO,
  splitISO,
  head,
);

const Sunday = 0;
const Saturday = 6;

/**
 * Check if the date is a holiday or a weekend
 *
 * @param {date} - the date to evaluate
 * @param {holidays} - array of holidays

 * @return whether or not date is weekend or is holiday
 */
export default (date, holidays) => {
  const isSaturday = date.getUTCDay() === Saturday;
  const isSunday = date.getUTCDay() === Sunday;
  const isHoliday = holidays.includes(calendarDate(date));

  return { isSaturday, isSunday, isHoliday };
};
