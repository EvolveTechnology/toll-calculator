import { halfHour, oneHour } from '../../../constants';

const intervalMaker = () => {};

describe('intervalMaker', () => {
  const baseDate = new Date(Date.UTC(2018, 0, 1));
  const baseDateUnix = baseDate.getTime();

  // generate timestamps separated by 30 minutes
  const everyThirtyMinutes = Array.from(
    { length: 48 },
    (_, i) => new Date(baseDateUnix + i * halfHour),
  );

  // generate timestamps separated by 60 minutes
  const everyHour = Array.from({ length: 24 }, (_, i) => new Date(baseDateUnix + i * oneHour));

  it('given a set of dates, it makes intervals of a given lenght', () => {
    const twoHours = 2 * oneHour;
    expect(intervalMaker(everyThirtyMinutes, oneHour)).toHaveLenght(24);
    expect(intervalMaker(everyHour, twoHours)).toHaveLenght(12);
  });
});
