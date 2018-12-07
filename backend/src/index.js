import axios from 'axios';
import feeByTimeOfDay from './services/feeByTimeOfDay';
import isTollFreeDay from './services/tollFreeDays';
import apiKey from './key';

const tz = new Date().getTimezoneOffset();
const now = new Date(new Date().getTime() - tz * 60 * 1000);

const year = now.getFullYear();
const { key } = apiKey;

const endpoint = `https://www.calendarindex.com/api/v1/holidays?country=SE&year=${year}&api_key=${key}`;

const calculateToll = async (day) => {
  const holidays = await axios.get(endpoint).then(({ data }) => data.response.holidays);
  const holidayDates = holidays.map(({ date }) => date.split(' ')[0]);

  console.log('The time is: ', day);
  console.log('Fee:', feeByTimeOfDay(day));
  console.log('Is it a fee free day?', isTollFreeDay(day, holidayDates));
};

calculateToll(now);
