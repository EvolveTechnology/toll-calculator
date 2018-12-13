import { HIGHEST, LOWEST, NONE } from "../constants";

// accumulate all types of vehicles
export const vehicleTypesAccumulator = vehicles =>
  vehicles.reduce(
    (prev, { type }) => (prev.includes(type) ? prev : prev.concat(type)),
    []
  );

// accumulate total fees
export const totalFeeAccumulator = fees =>
  Object.keys(fees).reduce((prev, day) => fees[day].totalFee + prev, 0);

// check if holidays
export const isHoliday = fee => fee.isHoliday;

// check if saturday or sunday
export const isWeekend = fee => fee.isSaturday || fee.isSunday;

export const isValidRegNum = regNum => {
  const regMatch = new RegExp(/[aA-zZ]{3}-[0-9]{3}/g);
  return regMatch.test(regNum);
};

const sortingFn = sort => {
  switch (sort) {
    case HIGHEST:
      return (entity, nextEntity) => nextEntity.totalFee - entity.totalFee;
    case LOWEST:
      return (entity, nextEntity) => entity.totalFee - nextEntity.totalFee;
    default:
      return null;
  }
};

export const sortingByTotalFees = (sort, arr) =>
  sort !== NONE ? arr.slice(0).sort(sortingFn(sort)) : arr;
