import { HIGHEST, NONE } from "../constants";

// accumulate all types of vehicles
export const vehicleTypesAccumulator = vehicles =>
  vehicles.reduce(
    (prev, { type }) => (prev.includes(type) ? prev : prev.concat(type)),
    []
  );

// accumulate total fees when input is object
export const objectTotalFeeAccumulator = obj =>
  Object.keys(obj).reduce((prev, day) => obj[day].totalFee + prev, 0);

// accumulate total fees when input is array
export const arrayTotalFeeAccumulator = arr =>
  arr.reduce((prev, { totalFee }) => totalFee + prev, 0);

// check if holidays
export const isHoliday = fee => fee.isHoliday;

// check if saturday or sunday
export const isWeekend = fee => fee.isSaturday || fee.isSunday;

export const isValidRegNum = regNum => {
  const regMatch = new RegExp(/[aA-zZ]{3}-[0-9]{3}/g);
  return regMatch.test(regNum);
};

const sortingFn = sort => {
  if (sort === HIGHEST) {
    return (entity, nextEntity) => nextEntity.totalFee - entity.totalFee;
  }
  return (entity, nextEntity) => entity.totalFee - nextEntity.totalFee;
};

export const sortingByTotalFees = (sort, arr) =>
  sort !== NONE ? arr.slice(0).sort(sortingFn(sort)) : arr;

export const capitalize = str => `${upperCase(str.charAt(0))}${str.slice(1)}`;

export const upperCase = str =>
  str && typeof str === "string" ? str.toUpperCase() : "";
