import feeByTimeOfDay from '..';
import { generateTimeStamps } from '../../../utils';
import { halfHour } from '../../../constants';

describe('feeByTimeOfDay', () => {
  const baseDate = new Date(Date.UTC(2018, 0, 1));

  // generate timestamps separated by 30 minutes
  const aWholeDay = generateTimeStamps(baseDate, halfHour, 48);

  // use this to store the initially proposed pricing scheme
  it('calculates tool fees for different hours', () => {
    expect(aWholeDay.map(hour => ({ fee: feeByTimeOfDay(hour), hour }))).toMatchSnapshot();
  });
});
