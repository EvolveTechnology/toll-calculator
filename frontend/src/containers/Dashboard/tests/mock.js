export const mockData = [
  {
    dates: ["2018-11-15 17:51:37", "2018-11-16 17:51:37"],
    fees: {
      "2018-11-15": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-15 17:51:37"],
        totalFee: 13,
        totalPasses: 1
      },
      "2018-11-16": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-16 17:51:37"],
        totalFee: 13,
        totalPasses: 1
      }
    },
    id: "063ad323-8c82-40e6-b509-af8f99c47324",
    regNum: "QNX-473",
    type: "Truck"
  },
  {
    dates: ["2018-11-17 17:51:37", "2018-11-18 17:51:37"],
    fees: {
      "2018-11-15": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-17 17:51:37"],
        totalFee: 13,
        totalPasses: 1
      },
      "2018-11-16": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-18 16:51:37"],
        totalFee: 18,
        totalPasses: 1
      }
    },
    id: "904ad323-9c22-40e6-b509-af8f99c47111",
    regNum: "ABC-123",
    type: "Car"
  }
];

export const expected = [
  {
    dates: ["2018-11-15 17:51:37", "2018-11-16 17:51:37"],
    fees: {
      "2018-11-15": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-15 17:51:37"],
        totalFee: 13,
        totalPasses: 1
      },
      "2018-11-16": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-16 17:51:37"],
        totalFee: 13,
        totalPasses: 1
      }
    },
    id: "063ad323-8c82-40e6-b509-af8f99c47324",
    regNum: "QNX-473",
    totalFee: 26,
    type: "Truck"
  },
  {
    dates: ["2018-11-17 17:51:37", "2018-11-18 17:51:37"],
    fees: {
      "2018-11-15": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-17 17:51:37"],
        totalFee: 13,
        totalPasses: 1
      },
      "2018-11-16": {
        chargeablePasses: 1,
        isHoliday: false,
        isSaturday: false,
        isSunday: false,
        isTollFreeVehicle: false,
        passes: ["2018-11-18 16:51:37"],
        totalFee: 18,
        totalPasses: 1
      }
    },
    id: "904ad323-9c22-40e6-b509-af8f99c47111",
    regNum: "ABC-123",
    totalFee: 31,
    type: "Car"
  }
];
