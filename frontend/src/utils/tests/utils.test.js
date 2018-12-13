import {
  vehicleTypesAccumulator,
  totalFeeAccumulator,
  isHoliday,
  isWeekend,
  isValidRegNum,
  sortingByTotalFees
} from "..";
import { expected } from "../../api/tests/mock";
import { HIGHEST, NONE, LOWEST } from "../../constants";

describe("vehicles types accumulator", () => {
  const vehicles = [{ type: "a" }, { type: "a" }, { type: "b" }];
  it("accumulates all vehicle types", () => {
    expect(vehicleTypesAccumulator(vehicles)).toEqual(["a", "b"]);
  });
});

describe("total fee accumulator", () => {
  const fees = {
    a: { totalFee: 10 },
    b: { totalFee: 20 },
    c: { totalFee: 30 }
  };
  it("accumulates all vehicle types", () => {
    expect(totalFeeAccumulator(fees)).toEqual(60);
  });
});

describe("simple checkers", () => {
  it("isHoliday and isWeekend", () => {
    expect(isHoliday({ isHoliday: true })).toEqual(true);
    expect(isWeekend({ isSaturday: true, isSunday: false })).toEqual(true);
    expect(isWeekend({ isSaturday: false, isSunday: true })).toEqual(true);
  });

  it("isValidRegNum", () => {
    const regNum = "AbC-123";
    const notRegNum = "Joseph";
    expect(isValidRegNum(regNum)).toEqual(true);
    expect(isValidRegNum(notRegNum)).toEqual(false);
  });
});

describe("sorts by total fee", () => {
  const toBeSorted = [{ totalFee: 0 }, { totalFee: 100 }, { totalFee: 10 }];

  it("sorts from highest to lowest", () => {
    expect(sortingByTotalFees(HIGHEST, toBeSorted)).toEqual([
      { totalFee: 100 },
      { totalFee: 10 },
      { totalFee: 0 }
    ]);
  });

  it("sorts from lowest to highest", () => {
    expect(sortingByTotalFees(LOWEST, toBeSorted)).toEqual([
      { totalFee: 0 },
      { totalFee: 10 },
      { totalFee: 100 }
    ]);
  });

  it("does nothing", () => {
    expect(sortingByTotalFees(NONE, toBeSorted)).toEqual([
      { totalFee: 0 },
      { totalFee: 100 },
      { totalFee: 10 }
    ]);
  });
});
