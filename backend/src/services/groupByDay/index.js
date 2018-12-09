import { sortDates, head, split } from '../../utils';

/**
 *  Group an array of dates by day
 *
 *
 * @param dates array of dates covering the same of different days
 * @return object with days as keys and relative dates as values
 */
export default dates => sortDates(dates).reduce((prev, date) => {
  const currentDay = head(split(date, ' '));
  // extract the current day from prev, named as passes
  // if its undefined assign it an empty array
  // this could be done as:
  // const passes = prev[currentDay] || []
  // but I leave to 'show off'
  const { [currentDay]: passes = [] } = prev;

  return { ...prev, [currentDay]: passes.concat(date) };
}, {});
