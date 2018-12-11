import isTollFreeDate from '..';
import { weekends, holidays } from './mock';

describe('tollFreeDays', () => {
  const days = ['2018-01-01', '2018-01-02 00:00:00', '2018-01-13', '2018-01-15'].map(
    day => new Date(day),
  );
  it('labels specified holidays as tollfree', () => {
    expect(days.map(date => isTollFreeDate(date, holidays))).toEqual([
      { isWeekend: false, isHoliday: true },
      { isWeekend: false, isHoliday: false },
      { isWeekend: true, isHoliday: true },
      { isWeekend: false, isHoliday: false },
    ]);
  });

  it('labels weekends as tollfree', () => {
    expect(weekends.map(date => isTollFreeDate(new Date(date), holidays))).toEqual([
      { isWeekend: true, isHoliday: false },
      { isWeekend: true, isHoliday: false },
      { isWeekend: true, isHoliday: false },
      { isWeekend: true, isHoliday: false },
    ]);
  });
});
