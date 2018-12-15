export const expected = {
  allTimeTotalFee: 13,
  error: false,
  id: "063ad323-8c82-40e6-b509-af8f99c47324",
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
    }
  },
  isTollFree: false,
  regNum: "QNX-473",
  results: [
    {
      chargeablePasses: 1,
      day: "2018-11-15",
      isHoliday: false,
      isSaturday: false,
      isSunday: false,
      isTollFreeVehicle: false,
      passes: ["2018-11-15 17:51:37"],
      totalFee: 13,
      totalPasses: 1
    }
  ],
  showSpinner: false,
  type: "Truck"
};

export const expectedAll = {
  error: false,
  loadingAll: false,
  vehicles: [
    {
      dates: ["2018-11-15 17:51:37"],
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
        }
      },
      id: "063ad323-8c82-40e6-b509-af8f99c47324",
      regNum: "QNX-473",
      totalFee: 13,
      type: "Truck"
    }
  ]
};

export const mockData = {
  dates: ["2018-11-15 17:51:37"],
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
    }
  },
  id: "063ad323-8c82-40e6-b509-af8f99c47324",
  regNum: "QNX-473",
  type: "Truck"
};
