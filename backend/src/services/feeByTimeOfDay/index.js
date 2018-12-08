import { inDayMinutes, inRange, partial } from '../../utils';
import fees from './fees';
import { FREE } from '../../constants';

const defaultValue = { fee: FREE };

/*
 *
 * Assumption:
 *
 * Beween 6:00 and 18:30, there is always a fee, different than 0
 *
 * In the proposed JAVA solution, there were gaps between 8:30 and 14:30.
 * Every first half hour charges 0. For example, 9:03 charges zero.
 *
 * My solution cancels that, and from 8:30 to 14:30 it charges 8 SEK
 *
 */
export default (date) => {
  const minutes = inDayMinutes(date);
  // partially apply our minutes variable to inRange
  const minutesInRange = partial(inRange)(minutes);

  // check if there is a range where our mintues are included
  // if not default to free
  const { fee } = fees.find(({ range }) => minutesInRange(range)) || defaultValue;
  return fee;
};
