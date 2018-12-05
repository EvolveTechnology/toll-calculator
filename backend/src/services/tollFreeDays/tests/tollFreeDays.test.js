import isTollFreeDate from '..';
import { weekends, holidays } from './mock';

const asUTC = ({ month, day, year }) => new Date(Date.UTC(year, month, day));
const asDates = arr => arr.map(asUTC);

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
