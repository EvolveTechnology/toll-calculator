import {
  generateTimeStamps,
  sortDates,
  pipe,
  partial,
  partialRight,
  inDayMinutes,
  inRange,
  head,
  split,
} from '.';
import { oneMinute, oneHour } from '../constants';

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

describe('sortDates', () => {
  const earlyStart = new Date('2018-01-01');
  const early = generateTimeStamps(earlyStart, oneMinute, 10);
  const lateStart = new Date('2019-01-01');
  const late = generateTimeStamps(lateStart, 3 * oneMinute, 10);

  const unOrdered = [...late, ...early];
  it('sorts the dates from earliest to latest', () => {
    expect(sortDates(unOrdered)).toEqual([...early, ...late]);
  });
});

describe('pipe', () => {
  const pass = jest.fn(a => a);
  const val = 2;
  const pipeResult = pipe(
    pass,
    pass,
    pass,
  )(val);

  it('pipes through', () => {
    expect(pass).toHaveBeenCalledTimes(3);
    expect(pass).toHaveBeenCalledWith(val);
  });

  it('returns the expected value', () => {
    expect(pipeResult).toEqual(val);
  });
});

describe('partial', () => {
  const fn = jest.fn((...args) => args.reduce((acc, val) => acc + val, 0));
  const firstArgs = [1, 2, 3];
  const secondArg = 4;
  const result = partial(fn)(...firstArgs)(secondArg);

  it('allows users to declare arguments in two batches from the left', () => {
    expect(result).toEqual(10);
    expect(fn).toHaveBeenCalledTimes(1);
    expect(fn).toHaveBeenCalledWith(1, 2, 3, 4);
  });
});

describe('partialRight', () => {
  const fn = jest.fn((a, b) => a - b);
  const firstArgs = -1;
  const secondArg = -1;
  const result = partialRight(fn)(firstArgs)(secondArg);

  it('allows users to declare arguments in two batches from the right', () => {
    expect(result).toEqual(0);
    expect(fn).toHaveBeenCalledTimes(1);
    expect(fn).toHaveBeenCalledWith(-1, -1);
  });
});

describe('inDayMinutes', () => {
  const fourMinutes = 4 * oneMinute;
  const fourMinutesPastMidnight = new Date(Date.UTC(2018, 1, 1) + fourMinutes);
  const threeHours = 3 * oneHour;
  const threeHoursPastMidnight = new Date(Date.UTC(2018, 1, 1) + threeHours);
  it('returns how many miliseconds since midnight have passed', () => {
    expect(inDayMinutes(fourMinutesPastMidnight)).toEqual(4);
    expect(inDayMinutes(threeHoursPastMidnight)).toEqual(180);
  });
});

describe('inRange', () => {
  const val = 4;
  const range = [0, 5];
  const outOfRange = 6;
  it('checks if values are in range', () => {
    expect(inRange(val, range)).toEqual(true);
    expect(inRange(outOfRange, range)).toEqual(false);
  });
});

describe('head', () => {
  const val = 4;
  const array = [val, 1, 2, 3, 5];
  it('checks if values are in range', () => {
    expect(head(array)).toEqual(val);
  });
});

describe('split', () => {
  const str = 'asjdkllasjd999s999ssksks';
  const pattern = '999';
  it('checks if values are in range', () => {
    expect(split(str, pattern)).toEqual(['asjdkllasjd', 's', 'ssksks']);
  });
});
