import groupByDay, { localTime } from '..';
import dates from './mock';

describe('groupByDay', () => {
  const expected = {
    '2018-11-02': [localTime('2018-11-02 12:14:04'), localTime('2018-11-02 15:41:19')],
    '2018-12-02': [localTime('2018-12-02 11:57:03')],
    '2018-11-24': [localTime('2018-11-24 10:57:05')],
    '2018-11-18': [localTime('2018-11-18 00:21:58')],
    '2018-11-29': [localTime('2018-11-29 19:28:18')],
    '2018-11-09': [localTime('2018-11-09 23:15:13')],
    '2018-11-05': [localTime('2018-11-05 00:17:20')],
    '2018-11-16': [localTime('2018-11-16 21:19:18')],
    '2018-11-26': [localTime('2018-11-26 20:45:54')],
    '2018-12-04': [localTime('2018-12-04 14:32:01')],
    '2018-12-05': [localTime('2018-12-05 03:40:54')],
    '2018-11-20': [localTime('2018-11-20 19:08:28')],
  };
  it('groups the dates by day', () => {
    expect(groupByDay(dates)).toEqual(expected);
  });
});
