import axios from 'axios';
import { calendarURI } from '../../constants';
import {
  head, split, partialRight, partial, flatten,
} from '../../utils';

const splitEmptySpace = partialRight(split)(' ');

// dynamic yearly based endpoint
export const holidaysURI = (key, year) => `${calendarURI}&year=${year}&api_key=${key}`;

// select the holidays object from the network response
export const selectHolidays = ({
  data: {
    response: { holidays },
  },
}) => holidays;

// format the string, since the api returns string + ' ' +hours
export const removeHoursPadding = ({ date }) => head(splitEmptySpace(date));

// make the api call and process the response
export const getHolidaysForYear = async (key, year) => axios
  .get(holidaysURI(key, year))
  .then(selectHolidays)
  .then(holidays => holidays.map(removeHoursPadding));

// use key and years and return all holidays
export default async (holidayKey, years) => {
  const withApiKey = partial(getHolidaysForYear)(holidayKey);
  const yearlyHolidays = await Promise.all(years.map(withApiKey));
  // since we get an array of arrays, flatten it
  return flatten(yearlyHolidays);
};
