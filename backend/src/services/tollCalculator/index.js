import { oneHour } from '../../constants';

/**
 * Calculate the total toll fee for one day
 *
 * @param vehicle - the vehicle
 * @param dates   - date and time of all passes on one day
 * @return - the total toll fee for that day
 */
export default function tollCalculator(vehicle, dates) {
  const feeIntervals = dates.reduce((intervals, curr) => {
    const asUnix = curr.getTime();
    const inOneHour = new Date(asUnix + oneHour);

    if (!intervals.length) {
      return [{ start: curr, end: inOneHour }];
    }

    const lastInterval = intervals.slice(-1);
    const inInterval = lastInterval.end < curr;

    if (inInterval) {
      return intervals;
    }

    return intervals.concat([{ start: curr, end: inOneHour }]);
  }, []);
  console.log(feeIntervals);
  return { vehicle, fee: dates.length };
}
