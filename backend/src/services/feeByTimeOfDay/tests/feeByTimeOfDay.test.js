import feeByTimeOfDay from '..';
import { generateTimeStamps } from '../../../utils';
import { halfHour, oneMinute } from '../../../constants';

const timeZoneOffset = (...args) => new Date(...args).getTimezoneOffset() * oneMinute;

describe('feeByTimeOfDay', () => {
  const baseDate = new Date(new Date(2018, 0, 1).getTime() - timeZoneOffset(2018, 0, 1));

  // generate timestamps separated by 30 minutes
  const aWholeDay = generateTimeStamps(baseDate, halfHour, 48);

  it('calculates tool fees for different hours', () => {
    expect(aWholeDay.map(hour => ({ fee: feeByTimeOfDay(hour), hour }))).toMatchSnapshot();
  });
});
