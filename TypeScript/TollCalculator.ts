import {
  fees,
  freeDays,
  freeWeekDays,
  tollFreeVehicles,
  Vehicle,
} from "./helpers";

class TollCalculator {
  useLocalTime = true;

  constructor({ useLocalTime = true } = {}) {
    this.useLocalTime = useLocalTime;
  }

  getTotalTollFee(vehicle: Vehicle, dates: Date[]) {
    const startDate = dates[0];
    let lastBillingDate = startDate;
    let totalFee = 0;

    dates.forEach((date: Date) => {
      if (!this.isSameDay(date, startDate)) {
        throw "Billings must be in the same day.";
      }

      const lastFee = this.getTollFee(vehicle, lastBillingDate);
      const fee = this.getTollFee(vehicle, date);
      const { minute: minutesSinceLastBilling } = this.getTime(
        new Date(date.getTime() - lastBillingDate.getTime())
      );

      console.log({ fee, lastFee });
      console.log(this.getTime(date));

      if (minutesSinceLastBilling <= 60) {
        if (fee >= lastFee) {
          totalFee += fee;
          lastBillingDate = date;
        }
      } else {
        totalFee += fee;
        lastBillingDate = date;
      }
      if (totalFee > 60) totalFee = 60;
    });

    return totalFee;
  }

  getTollFee(vehicle: Vehicle, date: Date) {
    const { hour, minute } = this.getTime(date);

    if (this.isTollFreeDate(date) || this.isTollFreeVehicle(vehicle)) return 0;

    return fees.find((fee) => fee.condition(hour, minute))?.value || 0;
  }

  private isTollFreeVehicle(vehicle: Vehicle) {
    return tollFreeVehicles.includes(vehicle);
  }

  private isTollFreeDate(date: Date) {
    const { year, month, day, weekDay } = this.getTime(date);

    if (freeWeekDays.includes(weekDay)) return true;

    return freeDays.some((it) => {
      return (
        it.year === year &&
        it.month === month &&
        (!it.days || it.days.includes(day))
      );
    });
  }

  private getTime(date: Date) {
    if (this.useLocalTime)
      return {
        year: date.getFullYear(),
        month: date.getMonth(),
        day: date.getDate(),
        weekDay: date.getDay(),
        hour: date.getHours(),
        minute: date.getMinutes(),
      };
    return {
      year: date.getUTCFullYear(),
      month: date.getUTCMonth(),
      day: date.getUTCDate(),
      weekDay: date.getUTCDay(),
      hour: date.getUTCHours(),
      minute: date.getUTCMinutes(),
    };
  }

  private isSameDay(date1: Date, date2: Date) {
    const { year: year1, month: month1, day: day1 } = this.getTime(date1);
    const { year: year2, month: month2, day: day2 } = this.getTime(date2);
    return year1 === year2 && month1 === month2 && day1 === day2;
  }
}

export default TollCalculator;
