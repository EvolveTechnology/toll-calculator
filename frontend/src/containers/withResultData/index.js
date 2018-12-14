import React from "react";
import { objectTotalFeeAccumulator, sortingByTotalFees } from "../../utils";

// relies on query selected vehicles to be unique
export function withResultData(
  Element,
  sorting,
  [{ fees, type: ignore, ...rest }]
) {
  const preSortedResults = Object.keys(fees).map(day => ({
    day,
    ...fees[day]
  }));

  const elementProps = {
    results: sortingByTotalFees(sorting, preSortedResults),
    isTollFree: Object.keys(fees).some(day => fees[day].isTollFreeVehicle),
    allTimeTotalFee: objectTotalFeeAccumulator(fees),
    ...rest
  };
  return <Element {...elementProps} />;
}

export default withResultData;
