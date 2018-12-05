import feeByTimeOfDay from '..';
import { halfHour } from '../../../constants';

describe('feeByTimeOfDay', () => {
  const baseDate = new Date(Date.UTC(2018, 0, 1));
  const baseDateUnix = baseDate.getTime();

  // generate timestamps separated by 30 minutes
  const aWholeDay = Array.from({ length: 48 }, (_, i) => new Date(baseDateUnix + i * halfHour));

  // use this to store the initially proposed pricing scheme
  it('calculates tool fees for different hours', () => {
    expect(aWholeDay.map(hour => ({ fee: feeByTimeOfDay(hour), hour }))).toMatchSnapshot();
  });
});
