import { head, partialRight, split } from '../../utils';

const toISO = date => date.toISOString();
const splitISO = partialRight(split)('T');

const utcWeekend = [0, 6];

/**
 * Check if the date is a holiday or a weekend
 *
 * @param {date} - the date to evaluate
 * @param {holidays} - array of holidays

 * @return whether or not date is weekend or is holiday
 */
export default (date, holidays) => {
  const isWeekend = utcWeekend.includes(date.getUTCDay());
  const holidayDate = head(splitISO(toISO(date)));
  const isHoliday = holidays.includes(holidayDate);

  if (isWeekend || isHoliday) return true;

  return false;
};
