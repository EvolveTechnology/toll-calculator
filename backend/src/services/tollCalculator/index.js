import intervalMaker from '../intervalMaker';
import feeByTimeOfDay from '../feeByTimeOfDay';
import { sortDates } from '../../utils';
import { oneHour, MAX_FEE } from '../../constants';

/**
 * Calculate the total toll fee for one day
 *
 * @param vehicle - the vehicle
 * @param dates   - date and time of all passes on one day
 * @return - the total toll fee for that day
 */
export default function tollCalculator(vehicle, dates) {
  const sortedPasses = sortDates(dates);
  const feeIntervals = intervalMaker(sortedPasses, oneHour);
  const fee = feeIntervals
    .map(({ start }) => feeByTimeOfDay(start))
    .reduce((acc, val) => (acc > MAX_FEE ? MAX_FEE : acc + val), 0);

  return { ...vehicle, fee };
}
