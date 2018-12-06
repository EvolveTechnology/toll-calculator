import intervalMaker from '..';
import { generateTimeStamps } from '../../../utils';
import { halfHour, oneHour } from '../../../constants';

describe('intervalMaker', () => {
  const baseDate = new Date(Date.UTC(2018, 0, 1));

  // generate timestamps separated by 30 minutes
  const everyThirtyMinutes = generateTimeStamps(baseDate, halfHour, 48);

  // generate timestamps separated by 60 minutes
  const everyHour = generateTimeStamps(baseDate, oneHour, 24);

  it('given a set of dates, it makes intervals of a given lenght', () => {
    const twoHours = 2 * oneHour;
    expect(intervalMaker(everyThirtyMinutes, oneHour)).toHaveLength(24);
    expect(intervalMaker(everyHour, twoHours)).toHaveLength(12);
  });
});
