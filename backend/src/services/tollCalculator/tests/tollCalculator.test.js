import tollCalculator from '..';
import groupByDay from '../../groupByDay';
import { generateTimeStamps, head } from '../../../utils';
import { oneHour } from '../../../constants';

describe('toll calculator', () => {
  const baseDate = new Date(new Date(2018, 0, 1));

  // generate timestamps separated by 60 minutes
  const dates = groupByDay(generateTimeStamps(baseDate, oneHour, 24));

  const vehicle = {
    type: 'Car',
  };
  const holidays = ['2018-01-01 00:00:00', '2018-01-06 00:00:00'];

  // For this initial test case, our case will cross a toll point every hour
  // and should accumulate more than 60 SEK in fees
  // but will only be charged 60 SEK
  it('calculates the toll for a given vehicle and dates', () => {
    const passes = head(Object.values(dates));
    expect(tollCalculator(vehicle, dates, holidays)).toEqual({
      chargeablePasses: 24,
      passes,
      totalPasses: passes.length,
      totalFee: 60,
    });
  });
});
