import axios from 'axios';
import { partial, flatten } from '../../utils';

// dynamic yearly based endpoint => the endpoint already contains the key
export const holidaysURI = (endpoint, year) => `${endpoint}&year=${year}`;

// select the holidays object from the network response
export const selectHolidays = ({
  data: {
    response: { holidays },
  },
}) => holidays;

export const getISO = ({ date: { iso } }) => iso;

// make the api call and process the response
export const getHolidaysForYear = async (key, year) => axios
  .get(holidaysURI(key, year))
  .then(selectHolidays)
  .then(holidays => holidays.map(getISO));

// use key and years and return all holidays
export default async (holidayKey, years) => {
  const withApiKey = partial(getHolidaysForYear)(holidayKey);
  const yearlyHolidays = await Promise.all(years.map(withApiKey));
  // since we get an array of arrays, flatten it
  return flatten(yearlyHolidays);
};
