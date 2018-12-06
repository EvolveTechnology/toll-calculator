import { generateTimeStamps } from '.';
import { oneMinute } from '../constants';

describe('timeStamp', () => {
  const start = new Date('2018-01-01');
  const span = 60 * oneMinute;
  const lenght = 4;
  const timeStamps = generateTimeStamps(start, span, lenght);

  it('creates 4 times stamps with one hour difference starting 2018-01-01 midnight', () => {
    expect(timeStamps).toHaveLength(4);
  });
});
