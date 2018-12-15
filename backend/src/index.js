import byDayFeeAccumulator from './services/byDayFeeAccumulator';
import groupByDay from './services/groupByDay';
import byYearHolidays from './services/holidaysAPI';
import getAllVehicles from './services/vehiclesAPI';
import { partial, getUniqueYears, find } from './utils';

/**
 *  given a vehicle registration number, return daily toll fees
 *
 * @param regNum registration plate string
 * @param holidayKey holidays api key
 * @param dataEndpoint endpoint to fetch vehicles
 * @return fees for the vehicle, daily
 */
export async function byVehicleCalculator(regNum, holidayKey, dataEndpoint) {
  // get all vehicles from an endpoint
  const vehicles = await getAllVehicles(dataEndpoint);
  // filter the one vehicle we care about, assuming regNums are unique
  const vehicle = find('regNum', regNum, vehicles);

  if (!vehicle) {
    return {
      regNum,
      id: null,
      fees: {},
      dates: [],
    };
  }

  // get the logged dates for the vehicle
  const { dates } = vehicle;
  // get unique years
  const years = getUniqueYears(dates);
  const holidays = await byYearHolidays(holidayKey, years);

  // group dates by day
  const byDay = groupByDay(dates);
  // inject daily fees
  const feeAccumulator = partial(byDayFeeAccumulator)(vehicle, byDay, holidays);
  const withFees = Object.keys(byDay).reduce(feeAccumulator, {});

  // append the vehicle with fees
  return { ...vehicle, fees: withFees };
}

/**
 *  Return daily toll fees for all vehicles
 *
 * @param holidayKey holidays api key
 * @param dataEndpoint endpoint to fetch vehicles
 * @return fees for ALL the vehicle, daily
 */
export async function allVehiclesCalculator(holidayKey, dataEndpoint) {
  // get all vehicles from an endpoint
  const vehicles = await getAllVehicles(dataEndpoint);

  const allDates = vehicles.reduce((prev, { dates }) => prev.concat(dates), []);
  const years = getUniqueYears(allDates);
  const holidays = await byYearHolidays(holidayKey, years);

  return vehicles.reduce((otherVehicles, vehicle) => {
    const { dates } = vehicle;
    const byDay = groupByDay(dates);
    const feeAccumulator = partial(byDayFeeAccumulator)(vehicle, byDay, holidays);
    // inject daily fees
    const withFees = Object.keys(byDay).reduce(feeAccumulator, {});

    // append the vehicle with fees
    return otherVehicles.concat({ ...vehicle, fees: withFees });
  }, []);
  // get the logged dates for the vehicle
}

module.exports = { byVehicleCalculator, allVehiclesCalculator };
