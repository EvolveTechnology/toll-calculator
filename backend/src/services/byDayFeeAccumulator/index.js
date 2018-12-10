import tollCalculator from '../tollCalculator';

/**
 *  given a vehicle registration number, return daily toll fees
 *
 * @param vehicle vehicle with a type
 * @param byDay dates grouped by day
 * @param holidays holidays for all years in dates included in byDay
 * @param otherDays previous calculations for day
 * @param day calculated toll for the day
 * @return fees for the vehicle, daily
 */
export default (vehicle, byDay, holidays, otherDays, day) => {
  // get the passes for this day
  const { [day]: passes } = byDay;
  // return the accumulated fees for other days, and append this day
  return { ...otherDays, [day]: tollCalculator(vehicle, { [day]: passes }, holidays) };
};
