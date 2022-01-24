import { inRange } from "lodash";

/************
 * Vehicles *
 ************/

export enum Vehicle {
  Car = "Car",
  Motorbike = "Motorbike",
  Tractor = "Tractor",
  Emergency = "Emergency",
  Diplomat = "Diplomat",
  Foreign = "Foreign",
  Military = "Military",
}

export const tollFreeVehicles = [
  Vehicle.Motorbike,
  Vehicle.Tractor,
  Vehicle.Emergency,
  Vehicle.Diplomat,
  Vehicle.Foreign,
  Vehicle.Military,
];

/********
 * Time *
 ********/

export enum WeekDay {
  Sun,
  Mon,
  Tue,
  Wed,
  Thu,
  Fri,
  Sat,
}

export enum Month {
  Jan,
  Feb,
  Mar,
  Apr,
  May,
  Jun,
  Jul,
  Aug,
  Sep,
  Oct,
  Nov,
  Dec,
}

export const freeWeekDays = [WeekDay.Sat, WeekDay.Sun];

export interface FreeDay {
  year: number;
  month: number;
  days?: number[];
}

export const freeDays: FreeDay[] = [
  { year: 2013, month: Month.Jan, days: [1] },
  { year: 2013, month: Month.Mar, days: [28, 29] },
  { year: 2013, month: Month.Apr, days: [1, 30] },
  { year: 2013, month: Month.May, days: [1, 8, 9] },
  { year: 2013, month: Month.Jun, days: [5, 6, 21] },
  { year: 2013, month: Month.Jul },
  { year: 2013, month: Month.Nov, days: [1] },
  { year: 2013, month: Month.Dec, days: [24, 25, 26, 31] },
];

/*********
 * Money *
 *********/

export interface Fee {
  value: number;
  condition: (hour: number, minute: number) => boolean;
}

export const fees: Fee[] = [
  {
    value: 8,
    condition: (h, m) =>
      (h === 6 && inRange(m, 0, 30)) ||
      (inRange(h, 8, 14) && inRange(m, 30, 60)) ||
      (h === 18 && inRange(m, 0, 30)),
  },
  {
    value: 13,
    condition: (h, m) =>
      (h === 6 && inRange(m, 30, 60)) ||
      (h === 8 && inRange(m, 0, 30)) ||
      (h === 15 && inRange(m, 0, 30)) ||
      (h == 17 && inRange(m, 0, 60)),
  },
  {
    value: 18,
    condition: (h, m) =>
      (h === 7 && inRange(m, 0, 60)) ||
      (h === 15 && inRange(m, 30, 60)) ||
      (h === 16 && inRange(m, 0, 60)),
  },
];
