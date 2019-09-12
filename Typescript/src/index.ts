import { isWeekend, isHoliday, isSameDate } from "./utils/date";
import { Time } from "./utils/Time";
import { Vehicle } from "Vehicle";
import { FeeBreakpoint } from "FeeBreakpoint";

const TOLL_FREE_VEHICLES: Vehicle[] = [
  "Motorbike",
  "Tractor",
  "Emergency",
  "Diplomat",
  "Foreign",
  "Military"
];

const MAXIMUM_TOLL_FEE = 60;

const FEE_BREAKPOINTS: FeeBreakpoint[] = [
  { time: new Time(6, 0), fee: 8 },
  { time: new Time(6, 30), fee: 13 },
  { time: new Time(7, 0), fee: 18 },
  { time: new Time(8, 0), fee: 13 },
  { time: new Time(8, 30), fee: 8 },
  { time: new Time(15, 0), fee: 13 },
  { time: new Time(15, 30), fee: 18 },
  { time: new Time(17, 0), fee: 13 },
  { time: new Time(18, 0), fee: 8 },
  { time: new Time(18, 30), fee: 0 }
];

export const getTollFee = (dates: Date[], vehicle: Vehicle): number => {
  if (isTollFreeVehicle(vehicle)) return 0;
  return getTimesIndexedByDay(dates).reduce((totalFee, { day, times }) => {
    if (isTollFreeDate(day)) return 0;
    const sortedDayTimes = times.sort((a, b) => (a.isAfter(b) ? 0 : 1));
    const dayFee = sortedDayTimes.reduce<{
      timesInHourPeriod: Time[];
      fee: number;
    }>(
      (entry, time, index) => {
        let timesInHourPeriod = entry.timesInHourPeriod;
        let fee = entry.fee;
        if (
          timesInHourPeriod.length > 0 &&
          time.diffInMinutes(timesInHourPeriod[0]) >= 60
        ) {
          fee += getMaximumTimeFee(timesInHourPeriod);
          timesInHourPeriod = [time];
        } else {
          timesInHourPeriod.push(time);
        }
        const isLastEntry = index === sortedDayTimes.length - 1;
        if (isLastEntry) {
          fee += getMaximumTimeFee(timesInHourPeriod);
          timesInHourPeriod = [];
        }
        return { timesInHourPeriod, fee };
      },
      { timesInHourPeriod: [], fee: 0 }
    ).fee;
    return totalFee + Math.min(MAXIMUM_TOLL_FEE, dayFee);
  }, 0);
};

const isTollFreeVehicle = (vehicle: Vehicle): boolean => {
  return TOLL_FREE_VEHICLES.includes(vehicle);
};

const isTollFreeDate = (date: Date): boolean => {
  return isWeekend(date) || isHoliday(date);
};

const getTimesIndexedByDay = (
  dates: Date[]
): Array<{ day: Date; times: Time[] }> => {
  return dates.reduce<Array<{ day: Date; times: Time[] }>>((result, date) => {
    const existingEntry = result.find(entry => isSameDate(date, entry.day));
    const time = Time.fromDate(date);
    if (existingEntry) {
      existingEntry.times.push(time);
    } else {
      result.push({ day: date, times: [time] });
    }
    return result;
  }, []);
};

const getMaximumTimeFee = (times: Time[]): number => {
  return Math.max(...times.map(time => getTimeFee(time)), 0);
};

const getTimeFee = (time: Time): number => {
  return (
    FEE_BREAKPOINTS.reduceRight<number | null>((fee, breakpoint) => {
      if (fee === null && time.isAfter(breakpoint.time)) {
        return breakpoint.fee;
      }
      return fee;
    }, null) || 0
  );
};
