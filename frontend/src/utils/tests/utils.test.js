import {
  partial,
  vehicleTypesAccumulator,
  objectTotalFeeAccumulator,
  arrayTotalFeeAccumulator,
  isHoliday,
  isWeekend,
  isValidRegNum,
  sortingByTotalFees,
  capitalize,
  upperCase,
  softTopScroll,
  safeClick
} from "..";

import { HIGHEST, NONE, LOWEST } from "../../constants";

describe("partial", () => {
  const fn = jest.fn((...args) => args.reduce((acc, val) => acc + val, 0));
  const firstArgs = [1, 2, 3];
  const secondArg = 4;
  const result = partial(fn)(...firstArgs)(secondArg);

  it("allows users to declare arguments in two batches from the left", () => {
    expect(result).toEqual(10);
    expect(fn).toHaveBeenCalledTimes(1);
    expect(fn).toHaveBeenCalledWith(1, 2, 3, 4);
  });
});

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
    expect(objectTotalFeeAccumulator(fees)).toEqual(60);
  });
});

describe("feeAccumulator", () => {
  const arr = [{ totalFee: 10 }, { totalFee: 20 }, { totalFee: 30 }];
  it("accumulates from the array", () => {
    expect(arrayTotalFeeAccumulator(arr)).toEqual(60);
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

describe("capitalize", () => {
  it("capitalizes", () => {
    expect(capitalize("joseph")).toEqual("Joseph");
    expect(capitalize("")).toEqual("");
  });
});

describe("upperCase", () => {
  it("upperCases", () => {
    expect(upperCase("joseph")).toEqual("JOSEPH");
    expect(upperCase("")).toEqual("");
    expect(upperCase(2)).toEqual("");
  });
});

describe("softTopScroll", () => {
  const scrollMock = jest.fn();
  const reqAnimMock = jest.fn();

  const scrollToMock = function(a, b) {
    return scrollMock(a, b);
  };

  const animToMock = function(...args) {
    return reqAnimMock(...args);
  };

  Object.defineProperty(window, "scrollTo", {
    value: scrollToMock
  });

  Object.defineProperty(window, "requestAnimationFrame", {
    value: animToMock
  });

  it("scrolls", () => {
    document.body.scrollTop = 8;
    softTopScroll();
    expect(reqAnimMock).toHaveBeenCalledWith(softTopScroll);
    expect(scrollMock).toHaveBeenCalledWith(0, 8 - 8 / 8);
  });
  it("does nothing if already at top", () => {
    document.body.scrollTop = 0;
    reqAnimMock.mockReset();
    scrollMock.mockReset();
    softTopScroll();
    expect(reqAnimMock).not.toHaveBeenCalled();
    expect(scrollMock).not.toHaveBeenCalled();
  });
});

describe("safeClick", () => {
  const onClick = jest.fn();
  const payload = [1, 2, 3];
  const eventData = { target: { value: "sjsjs" } };
  it("returns calls the onClick action", () => {
    const ev = safeClick(onClick, payload);
    expect(ev).toBeInstanceOf(Function);
    expect(onClick).not.toHaveBeenCalled();
    ev(eventData);
    expect(onClick).toHaveBeenCalledWith(payload);
  });
  it("returns null for bad onClick action", () => {
    onClick.mockReset();
    const evNull = safeClick(undefined, payload);
    expect(evNull).toBeInstanceOf(Function);
    expect(onClick).not.toHaveBeenCalled();
    const ret = evNull(eventData);
    expect(onClick).not.toHaveBeenCalled();
    expect(ret).toEqual(null);
  });
});
