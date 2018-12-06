import { generateTimeStamps } from '.';
import { oneMinute } from '../constants';

describe('timeStamp', () => {
  const start = new Date('2018-01-01');
  const span = 60 * oneMinute;
  const length = 4;
  const timeStamps = generateTimeStamps(start, span, length);

  it('creates 4 times stamps with one hour difference starting 2018-01-01 midnight', () => {
    expect(timeStamps).toHaveLength(4);
  });

  it('creates a time difference equal to span for every contiguous element', () => {
    // remember that the intervals between timestamps are lenght - 1;
    const [last] = timeStamps.slice(-1);
    const [first] = timeStamps;
    const totalSpan = last - first;
    expect(totalSpan).toEqual(span * (length - 1));
  });
});
