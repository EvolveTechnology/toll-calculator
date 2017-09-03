import * as t from "./types"

export const config = {
  fees: {
    cost: [0, 8, 13, 18],
    max: 60,
    matrix: [
      [["0800", "0859"], t.COST.LOW],
      [["0900", "1129"], t.COST.MID],
      [["1130", "1329"], t.COST.HIGH],
      [["1330", "1559"], t.COST.LOW],
      [["1600", "1829"], t.COST.HIGH],
    ],
  },
  // free entries
  whitelist: {
    dates: [t.DATE.XMAS, t.DATE.NEWYEAR],
    days: [t.DAY.SATURDAY, t.DAY.SUNDAY],
    vehicles: [t.VEHICLE.MILITARY, t.VEHICLE.DIPLOMAT],
  },
  // illegal entries (not implemented)
  watchlist: {},
}
