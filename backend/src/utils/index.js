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

export default generateTimeStamps;
