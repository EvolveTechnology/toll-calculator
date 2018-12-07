import tollCalculator from '..';
import { generateTimeStamps } from '../../../utils';
import { oneHour, oneMinute } from '../../../constants';

describe('toll calculator', () => {
  const timeZoneOffset = new Date().getTimezoneOffset() * oneMinute;
  const baseDate = new Date(new Date(2018, 0, 1) - timeZoneOffset);

  // generate timestamps separated by 60 minutes
  const dates = generateTimeStamps(baseDate, oneHour, 24);

  const vehicle = {
    type: 'Car',
  };

  // For this initial test case, our case will cross a toll point every hour
  // and should accumulate more than 60 SEK in fees
  // but will only be charged 60 SEK
  it('calculates the toll for a given vehicle and dates', () => {
    expect(tollCalculator(vehicle, dates)).toEqual({
      chargeablePasses: 24,
      passes: 24,
      totalFee: 60,
      ...vehicle,
    });
  });
});
