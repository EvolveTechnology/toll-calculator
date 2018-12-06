import tollCalculator from '..';
import { halfHour } from '../../../constants';

describe('toll calculator', () => {
  const baseDate = new Date(Date.UTC(2018, 0, 1));
  const baseDateUnix = baseDate.getTime();

  // generate timestamps separated by 60 minutes
  const dates = Array.from({ length: 24 }, (_, i) => new Date(baseDateUnix + i * halfHour));

  const vehicle = {
    type: 'Car',
  };

  // For this initial test case, our case will cross a toll point every hour
  // and should accumulate more than 60 SEK in fees
  // but will only be charged 60 SEK
  it('calculates the toll for a given vehicle and dates', () => {
    expect(tollCalculator(vehicle, dates)).toEqual({ fee: 60, ...vehicle });
  });
});
