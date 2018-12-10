/**
 * Generates an array of timestamps separated by span
 *
 * @param start - the start date
 * @param span  - separation between timestamps
 * @param length  - amount of timestamps -> intervals = lenght - 1;
 * @return - array where each element is start + index * span
 */
export const generateTimeStamps = (start, span, length) => {
  const startDateUnix = start.getTime();

  // generate timestamps separated by 60 minutes
  return Array.from({ length }, (_, index) => new Date(startDateUnix + index * span));
};

/**
 * Sort an array of dates
 *
 * @param dates - array of dates
 * @return - sorted array of dates from earliest to latest
 */
export const sortDates = dates => dates.slice(0).sort();

/**
 * take a result, a function and an index and apply the result to the function
 * in one of two ways
 *
 * A: spread the previous result, so it passes as many arguments
 * B: simply evaluate the funtion with the previous result
 *
 * @param prev - previous result
 * @param fn - function to receive the result
 * @param index - a numeric index
 * @return the resulf of appliying the previous result, by either A or B
 */
const folder = (prev, fn, index) => (index === 0 ? fn(...prev) : fn(prev));

/**
 * pass the a value and its results through a group of functions
 *
 * @param functions - funtions
 * @param args - initial arguments
 * @return the resulf of passing the initial values through the functions
 */
export const pipe = (...fns) => (...args) => fns.reduce(folder, args);

/**
 *  partially apply arguments to a function
 *
 * @param f - funtion
 * @param a - first group of arguments
 * @param b - second group of arguments
 * @return result of appliying the two group of arguments against f
 */
export const partial = f => (...a) => (...b) => f(...a, ...b);

/**
 *  partially apply arguments to a function
 *
 * @param f - funtion
 * @param b - second group of arguments
 * @param a - first group of arguments
 * @return result of appliying the two group of arguments against f
 */
export const partialRight = f => (...b) => (...a) => f(...a, ...b);

/**
 *  given a date, take the hour and minutes and count minutes from midnight
 *  midnight yields 0 minutes
 *
 *
 * @param date - a date
 * @return minutes since midnight
 */
export const inDayMinutes = (date) => {
  const hours = date.getUTCHours();
  const minutes = date.getUTCMinutes();
  const totalMinutes = hours * 60 + minutes;
  return totalMinutes;
};

/**
 *  check if a given value is included in a range
 *
 *
 * @param val numeric value to test
 * @param range array with lower and upper bound
 * @return minutes since midnight
 */
export const inRange = (val, [lower, upper]) => val <= upper && val >= lower;

/**
 *  take the first element of an array - unwrapped
 *  equivalent to array[0]
 *
 *
 * @param array target array
 * @return first element
 */
export const head = ([first]) => first;

/**
 *  split a string at the pattern
 *
 *
 * @param str target string
 * @param pattern where to break
 * @return first element
 */
export const split = (str, pattern) => str.split(pattern);

/**
 *  flatten a nested array
 *
 * @param nested array
 * @return flattened array
 */
export const flatten = nested => [].concat(...nested);

/**
 *  From string to date without time zone influence
 *
 * @param date a string to convert with fidelity
 * @return flattened array
 */
const getTimezoneOffset = date => new Date(date).getTimezoneOffset() * 60 * 1000;
const getTime = date => new Date(date).getTime();
export const localTime = date => new Date(getTime(date) - getTimezoneOffset(date));

/**
 *  given an array of dates, get unique years
 *
 * @param dates array of dates
 * @return unique years in dates
 */
export const getUniqueYears = dates => dates.reduce((prev, curr) => {
  const year = localTime(curr).getFullYear();
  return prev.includes(year) ? prev : prev.concat(year);
}, []);

/**
 *  find a key with value in array
 *
 * @param key key from the array
 * @param value value to find
 * @param array target array
 * @return first element where key equals value
 */
export const find = (key, value, array) => array.find(({ [key]: val }) => value === val);
