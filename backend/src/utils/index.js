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
