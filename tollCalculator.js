const moment = require("moment");
const Vehicle = require("./vehicle");
const TollFreeVehicles = require("./constants/tollFreeVehicle");
const tollFreeDates = require("./constants/tollFreeDates");
const tollFreeDays = require("./constants/tollFreeDays");
const tollAmount = require("./constants/tollAmount");
const datesHelpers = require("./dates")

class TollCalculator {
  getTollFee(dateOrVehicle, datesOrVehicle) {
    if (dateOrVehicle instanceof Date && datesOrVehicle instanceof Vehicle) {
      return this.getTollFeeForDate(dateOrVehicle, datesOrVehicle);
    } else if (
      dateOrVehicle instanceof Vehicle &&
      Array.isArray(datesOrVehicle) &&
      datesOrVehicle.every((date) => date instanceof Date)
    ) {
      return this.getTotalTollFee(dateOrVehicle, datesOrVehicle);
    } else {
      throw "Cannot calculate toll:Invalid args";
    }
  }

  getTotalTollFee(vehicle, dates) {
    let totalFee = 0;
    const sortedDates = datesHelpers.sortDatesByAscOrder(dates);
    let intervalStart = sortedDates[0];
    let tempFee = this.getTollFee(intervalStart, vehicle);
    for (const date of sortedDates) {
      const nextFee = this.getTollFee(date, vehicle);
      let minutes = datesHelpers.getMinutesDifferenceBetweenDates(date, intervalStart);
      if (minutes <= 60) {
        if (totalFee > 0) totalFee -= tempFee;
        if (nextFee >= tempFee) tempFee = nextFee;
        totalFee += tempFee;
        tempFee = nextFee;
      } else {
        totalFee += nextFee;
        intervalStart = date;
      }
    }
    if (totalFee > 60) totalFee = 60;
    return totalFee;
  }

  getTollFeeForDate(date, vehicle) {
    if (this.isTollFreeDate(date) || this.isTollFreeVehicle(vehicle)) return 0;
    const hour = Math.abs(moment(date).format("HH"));
    const minute = Math.abs(moment(date).format("mm"));
    return tollAmount[hour] ? tollAmount[hour][minute] || 0 : 0;
  }

  isTollFreeDate(date) {
    const year = moment(date).format("YYYY");
    const month = moment(date).format("MMMM");
    const day = Math.abs(moment(date).format("DD"));
    const dayOfWeek = moment(date).format("dddd");

    if (tollFreeDays.includes(dayOfWeek)) return true;

    if (
      tollFreeDates[year] &&
      tollFreeDates[year][month] &&
      tollFreeDates[year][month][day]
    )
      return true;

    return false;
  }

  isTollFreeVehicle(vehicle) {
    if (vehicle == null) return false;
    const vehicleType = vehicle.getType();
    return TollFreeVehicles.includes(vehicleType);
  }
}


module.exports = TollCalculator;