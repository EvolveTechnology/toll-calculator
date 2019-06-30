import {
  isSameDay,
  getISODay,
  addDays,
  getYear,
  getHours,
  parseISO,
  isWithinInterval,
  endOfDay,
  startOfMinute,
  endOfMinute,
  setHours,
  setMinutes
} from "date-fns";

const MAX_TOLL = 60;

/**
 * Calculate the total toll fee for one day
 *
 * @param vehicle - the vehicle object
 * @param dates   - a list of date and time ISO timestamps of all passes on one day
 * @return - the total toll fee for that day
 */
export function calculateDailyToll(vehicle, dates) {
  if (isTollFreeVehicle(vehicle.type)) {
    return 0;
  }

  const tolls = dates.reduce((ds, d) => {
    const date = parseISO(d);
    const hour = getHours(date);
    const currentToll = ds[hour];
    const toll = calculateHourlyToll(date);

    return toll > currentToll || currentToll == undefined
      ? { ...ds, [hour]: toll }
      : ds;
  }, {});

  const toll = Object.values(tolls).reduce((sum, toll) => sum + toll);
  return toll > MAX_TOLL ? MAX_TOLL : toll;
}

const intervals = [
  { start: { hour: 0, minute: 0 }, end: { hour: 6, minute: 0 }, toll: 0 },
  { start: { hour: 6, minute: 0 }, end: { hour: 6, minute: 30 }, toll: 8 },
  { start: { hour: 6, minute: 30 }, end: { hour: 7, minute: 0 }, toll: 13 },
  { start: { hour: 7, minute: 0 }, end: { hour: 8, minute: 0 }, toll: 18 },
  { start: { hour: 8, minute: 0 }, end: { hour: 8, minute: 30 }, toll: 13 },
  { start: { hour: 8, minute: 30 }, end: { hour: 15, minute: 0 }, toll: 8 },
  { start: { hour: 15, minute: 0 }, end: { hour: 15, minute: 30 }, toll: 13 },
  { start: { hour: 15, minute: 30 }, end: { hour: 17, minute: 0 }, toll: 18 },
  { start: { hour: 17, minute: 0 }, end: { hour: 18, minute: 0 }, toll: 13 },
  { start: { hour: 18, minute: 0 }, end: { hour: 18, minute: 30 }, toll: 8 },
  { start: { hour: 18, minute: 30 }, end: "end-of-day", toll: 0 }
];

const startInterval = (date, interval) =>
  startOfMinute(
    setMinutes(setHours(date, interval.start.hour), interval.start.minute)
  );

const endInterval = (date, interval) =>
  interval.end == "end-of-day"
    ? endOfDay(date)
    : endOfMinute(
        setMinutes(setHours(date, interval.end.hour), interval.end.minute - 1)
      );

function calculateHourlyToll(date) {
  if (isHoliday(date)) {
    return 0;
  }

  return intervals.find(interval =>
    isWithinInterval(date, {
      start: startInterval(date, interval),
      end: endInterval(date, interval)
    })
  ).toll;
}

function isTollFreeVehicle(vehicle) {
  switch (vehicle.type) {
    case "motorcycle":
    case "tractor":
    case "emergency":
    case "diplomat":
    case "foreign":
    case "military":
      return true;

    default:
      return false;
  }
}

/**
 * author: Kodie Grantham
 * licensing: MIT License
 * https://github.com/kodie/moment-holiday/blob/master/locale/easter.js
 */
function calculateEaster(y) {
  var c = Math.floor(y / 100);
  var n = y - 19 * Math.floor(y / 19);
  var k = Math.floor((c - 17) / 25);
  var i = c - Math.floor(c / 4) - Math.floor((c - k) / 3) + 19 * n + 15;
  i = i - 30 * Math.floor(i / 30);
  i =
    i -
    Math.floor(i / 28) *
      (1 -
        Math.floor(i / 28) *
          Math.floor(29 / (i + 1)) *
          Math.floor((21 - n) / 11));
  var j = y + Math.floor(y / 4) + i + 2 - c + Math.floor(c / 4);
  j = j - 7 * Math.floor(j / 7);
  var l = i - j;
  var m = 3 + Math.floor((l + 40) / 44);
  var d = l + 28 - 31 * Math.floor(m / 4);
  return new Date(y, m - 1, d);
}

const isHoliday = date =>
  (easter =>
    getISODay(date) == 6 ||
    getISODay(date) == 7 ||
    isSameDay(date, addDays(easter, 1)) ||
    isSameDay(date, addDays(easter, -2)) ||
    isSameDay(date, addDays(easter, 39)) ||
    isSameDay(date, new Date(getYear(date), 5, 6)) ||
    isSameDay(date, new Date(getYear(date), 11, 25)) ||
    isSameDay(date, new Date(getYear(date), 11, 26)) ||
    isSameDay(date, new Date(getYear(date), 0, 1)) ||
    isSameDay(date, new Date(getYear(date), 0, 6))
      ? true
      : false)(calculateEaster(getYear(date)));

export const __test__isHoliday = isHoliday;
export const __test__isTollFreeVehicle = isTollFreeVehicle;
export const __test__calculateHourlyToll = calculateHourlyToll;
