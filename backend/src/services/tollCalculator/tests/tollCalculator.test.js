import tollCalculator from '..';
import groupByDay from '../../groupByDay';
import { head } from '../../../utils';
import dates from './mock';

describe('toll calculator', () => {
  // generate timestamps separated by 60 minutes

  const byDay = groupByDay(dates);

  const vehicle = {
    type: 'Car',
  };

  const freeVehicle = {
    type: 'Diplomat',
  };
  const holidays = ['2018-01-01', '2018-01-06'];

  // For this initial test case, our case will cross a toll point every hour
  // and should accumulate more than 60 SEK in fees
  // but will only be charged 60 SEK
  it('calculates the toll for a given vehicle and dates', () => {
    const passes = head(Object.values(byDay));
    expect(tollCalculator(vehicle, byDay, holidays)).toEqual({
      chargeablePasses: 24,
      passes,
      totalPasses: passes.length,
      totalFee: 60,
      isTollFreeVehicle: false,
      isWeekend: false,
      isHoliday: false,
    });
  });

  it('calculates the toll for a fee free vehicle and dates', () => {
    const passes = head(Object.values(byDay));
    expect(tollCalculator(freeVehicle, byDay, holidays)).toEqual({
      chargeablePasses: 0,
      passes,
      totalPasses: passes.length,
      totalFee: 0,
      isTollFreeVehicle: true,
      isWeekend: false,
      isHoliday: false,
    });
  });
});
