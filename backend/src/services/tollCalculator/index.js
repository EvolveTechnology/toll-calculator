import intervalMaker from '../intervalMaker';
import feeByTimeOfDay from '../feeByTimeOfDay';
import tollFreeDays from '../tollFreeDays';
import tollFreeVehicles from '../tollFreeVehicles';

import { sortDates, head } from '../../utils';
import { oneHour, MAX_FEE } from '../../constants';

/**
 * Adds an accumulated value to a current value
 *
 * @param acc - accumulated value
 * @param val - current value
 * @return fee - the result, capped at MAX_FEE
 */
export const dailyFeeAccumulator = (acc, val) => (acc >= MAX_FEE ? MAX_FEE : acc + val);

/**
 * given an object with a start key, select it
 *
 * @param {start} - an object with at least a start key
 * @return - the value of the start key
 */
export const selectStart = ({ start }) => start;

/**
 * Calculate the total toll fee for one day
 *
 * @param vehicle - the vehicle
 * @param dates   - date and time of all passes on one day
 * @return - the total toll fee for that day
 */
export default function tollCalculator(vehicle, dates, holidays) {
  const isTollFreeVehicle = tollFreeVehicles(vehicle);

  // extract data from dates = { [day] : passes}
  const date = head(Object.keys(dates));
  const { [date]: passes } = dates;

  const day = new Date(date);
  const isTollFreeDay = tollFreeDays(day, holidays);

  if (isTollFreeVehicle || isTollFreeDay) {
    return {
      totalFee: 0,
      passes,
      totalPasses: passes.length,
      chargeablePasses: 0,
      isTollFreeVehicle,
      isTollFreeDay,
    };
  }

  const sortedPasses = sortDates(passes);
  const feeIntervals = intervalMaker(sortedPasses, oneHour);
  const chargeableTimes = feeIntervals.map(selectStart);
  const fees = chargeableTimes.map(feeByTimeOfDay);
  const totalFee = fees.reduce(dailyFeeAccumulator, 0);

  return {
    totalFee,
    passes,
    totalPasses: passes.length,
    chargeablePasses: chargeableTimes.length,
    isTollFreeVehicle,
    isTollFreeDay,
  };
}
