import isTollFreeDate from '..';
import { weekends, holidays } from './mock';

// the time zone is dynamic
const timeZoneOffset = (...args) => new Date(...args).getTimezoneOffset() * 60 * 1000;
const getDateTime = (...args) => new Date(...args).getTime();
const localTime = (...args) => getDateTime(...args) - timeZoneOffset(...args);
const toDate = ({ year, month, day }) => new Date(localTime(year, month, day));
const asDates = arr => arr.map(toDate);

describe('tollFreeDays', () => {
  it('labels specified holidays as tollfree', () => {
    expect(
      asDates(holidays).map(date => ({ isTollFree: isTollFreeDate(date), date })),
    ).toMatchSnapshot();
  });

  it('labels weekends as tollfree', () => {
    expect(
      asDates(weekends).map(date => ({ isTollFree: isTollFreeDate(date), date })),
    ).toMatchSnapshot();
  });
});
