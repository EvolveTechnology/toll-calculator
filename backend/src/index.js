import axios from 'axios';
import tollCalculator from './services/tollCalculator';

import apiKey from './key';

const tz = new Date().getTimezoneOffset();
const now = new Date(new Date().getTime() - tz * 60 * 1000);

const year = now.getFullYear();
const { key } = apiKey;

const endpoint = `https://www.calendarindex.com/api/v1/holidays?country=SE&year=${year}&api_key=${key}`;

// a given set of data
const myCar = { type: 'Car' };
const tollPasses = [now];

const calculateToll = async (vehicle, dates) => {
  const holidays = await axios.get(endpoint).then(({ data }) => data.response.holidays);
  const holidayDates = holidays.map(({ date }) => date.split(' ')[0]);
  const result = tollCalculator(vehicle, dates, holidayDates);
  /* eslint-disable-next-line */
  console.log(result);
  return result;
};

calculateToll(myCar, tollPasses);
