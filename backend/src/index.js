import axios from 'axios';
import tollCalculator from './services/tollCalculator';
import groupByDay from './services/groupByDay';
import { head, flatten, split } from './utils';

import apiKey from './key';

const { key, dataEndpoint } = apiKey;

const calendarURI = 'https://www.calendarindex.com/api/v1/holidays?country=SE';
// dynamic yearly based endpoint
const holidaysURI = year => `${calendarURI}&year=${year}&api_key=${key}`;
// select the holidays object from the network response
const selectHolidays = ({ data }) => data.response.holidays;
// format the string, since the api returns string + ' ' +hours
const removeHoursPadding = ({ date }) => head(split(date, ' '));
// make the api call and process the response
const getHolidaysForYear = year => axios
  .get(holidaysURI(year))
  .then(selectHolidays)
  .then(holidays => holidays.map(removeHoursPadding));

/**
 *  given an array of dates, get unique years
 *
 * @param dates array of dates
 * @return unique years in dates
 */
const getUniqueYears = dates => dates.reduce((prev, curr) => {
  const year = new Date(curr).getFullYear();
  return prev.includes(year) ? prev : prev.concat(year);
}, []);

/**
 *  given a vehicle registration number, return daily toll fees
 *
 * @param targetRegNum registration plate string
 * @return fees for the vehicle, daily
 */
const calculateToll = async (targetRegNum) => {
  // get all vehicles from an endpoint
  const vehicles = await axios.get(dataEndpoint).then(({ data }) => data);
  // filter the one vehicle we care about, assuming regNums are unique
  const vehicle = vehicles.find(({ regNum }) => regNum === targetRegNum);
  // get the logged dates for the vehicle
  const { dates } = vehicle;

  // get unique years
  const years = getUniqueYears(dates);
  // get yearly holidays for the years present in dates
  const yearlyHolidays = await Promise.all(years.map(getHolidaysForYear));
  // since we get an array of arrays, flatten it
  const holidays = flatten(yearlyHolidays);

  // group dates by day
  const byDay = groupByDay(dates);

  // inject daily fees
  const withFees = Object.keys(byDay).reduce((otherDays, day) => {
    // get the passes for this day
    const { [day]: passes } = byDay;
    // return the accumulated fees for other days, and append this day
    return { ...otherDays, [day]: tollCalculator(vehicle, { [day]: passes }, holidays) };
  }, {});

  // append the vehicle with fees
  const result = { ...vehicle, fees: withFees };
  /* eslint-disable-next-line */
  console.log(result);
  return result;
};

const myCar = 'FJK-136';
calculateToll(myCar);
