import feeByTimeOfDay from '..';
import { generateTimeStamps } from '../../../utils';
import { halfHour, oneMinute } from '../../../constants';

const timeZoneOffset = (...args) => new Date(...args).getTimezoneOffset() * oneMinute;

describe('feeByTimeOfDay', () => {
  const baseDate = new Date(new Date(2018, 0, 1).getTime() - timeZoneOffset(2018, 0, 1));

  // generate timestamps separated by 30 minutes
  const aWholeDay = generateTimeStamps(baseDate, halfHour, 48);

  // use this to store the initially proposed pricing scheme
  it('calculates tool fees for different hours', () => {
    expect(aWholeDay.map(hour => ({ fee: feeByTimeOfDay(hour), hour }))).toMatchSnapshot();
  });
});

describe('Correction for poor initial implementation', () => {
  const baseDate = new Date(Date.UTC(2018, 0, 4, 8, 58) - timeZoneOffset(2018, 0, 1));
  const trickLimit = generateTimeStamps(baseDate, oneMinute, 5);
  const anotherBase = new Date(Date.UTC(2018, 0, 4, 9, 28) - timeZoneOffset);
  const secondTrickLimit = generateTimeStamps(anotherBase, oneMinute, 5);

  it('corrects poor initial implementation between 8 and 14', () => {
    expect(trickLimit.map(feeByTimeOfDay)).toEqual([8, 8, 8, 8, 8]);
  });

  it('every second half hour charges', () => {
    expect(secondTrickLimit.map(feeByTimeOfDay)).toEqual([8, 8, 8, 8, 8]);
  });
});
