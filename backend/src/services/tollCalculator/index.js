/**
 * Calculate the total toll fee for one day
 *
 * @param vehicle - the vehicle
 * @param dates   - date and time of all passes on one day
 * @return - the total toll fee for that day
 */
export default function tollCalculator(vehicle, dates) {
  return { vehicle, fee: dates.length };
}
