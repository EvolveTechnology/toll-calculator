import nanoid from "nanoid"

import { assert, clamp, groupBy, includes, not, sum, values } from "./utils"
import { matchesDate, sameHour, toDate, toTime, withinTime } from "./utils/time"

// whitelist helpers
const isFreeDay = list => d => includes(d.getDay(), list.days)
const isFreeDate = list => d => !!list.dates.find(matchesDate(d))
const isFreeVehicle = list => v => includes(v.type, list.vehicles)

class TollCalculator {
  constructor(config) {
    this.id = nanoid()
    this.config = config
  }

  getFee(vehicle, dates = []) {
    assert(vehicle, "No vehicle")
    const { fees, whitelist } = this.config

    if (isFreeVehicle(whitelist)(vehicle)) {
      return 0
    }
    // group dates by day and calc fee
    const days = groupBy(toDate, dates)
    const feePerDay = values(days).map(this.calcFee.bind(this))

    // limit each day to max fee and sum days
    const fee = feePerDay.map(fee => clamp(fee, fees.max)).reduce(sum, 0)
    return fee
  }

  calcFee(dates = []) {
    const { fees, whitelist } = this.config
    const fee = dates
      .filter(not(sameHour))
      .filter(not(isFreeDay(whitelist)))
      .filter(not(isFreeDate(whitelist)))
      .map(lookupFee)
      .reduce(sum, 0)

    // find cost in time matrix
    function lookupFee(date) {
      assert(date instanceof Date, "Invalid date")
      const cell = fees.matrix.find(withinTime(date))
      return cell ? fees.cost[cell[1]] : 0
    }

    return fee
  }
}

export default TollCalculator
