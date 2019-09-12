const STATIC_HOLIDAYS: { month: number; day: number }[] = [
  { month: 1, day: 1 }, // New years day
  { month: 1, day: 6 }, // Epiphany
  { month: 5, day: 1 }, // First of May
  { month: 6, day: 6 }, // National day
  { month: 12, day: 25 }, // Christmas day
  { month: 12, day: 26 } // Boxing day
];

export const isWeekend = (date: Date): boolean => {
  const day = date.getDay();
  return day === 6 || day === 0;
};

export const isSameDate = (date: Date, target: Date): boolean => {
  return (
    date.getFullYear() === target.getFullYear() &&
    date.getDate() === target.getDate() &&
    date.getMonth() === target.getMonth()
  );
};

const addDays = (date: Date, days: number): Date => {
  return new Date(new Date(date).setDate(date.getDate() + days));
};

export const isHoliday = (date: Date): boolean => {
  const year = date.getFullYear();
  const easter = getEasterDate(year);
  const easterFriday = addDays(easter, -2);
  const easterMonday = addDays(easter, 1);
  const ascensionDay = addDays(easter, 39);
  const pentecostDay = addDays(easter, 49);
  const allSaintsDay = getAllSaintsDate(year);
  const midsummerDay = getMidsummerDate(year);
  const staticHolidays = STATIC_HOLIDAYS.map(
    ({ month, day }) => new Date(year, month - 1, day)
  );
  const holidaysOfTheYear: Date[] = [
    ...staticHolidays,
    easter,
    easterFriday,
    easterMonday,
    ascensionDay,
    pentecostDay,
    allSaintsDay,
    midsummerDay
  ];
  return holidaysOfTheYear.some(holiday => isSameDate(date, holiday));
};

const getEasterDate = (year: number): Date => {
  const [month, day] = getEaster(year);
  return new Date(year, month - 1, day);
};

const getAllSaintsDate = (year: number): Date => {
  // Saturday between October 31st and November 6th
  const firstDate = new Date(`${year}-10-31`);
  return addDays(firstDate, 6 - (firstDate.getDay() % 7));
};

const getMidsummerDate = (year: number): Date => {
  // Saturday between June 20th and June 26th
  const firstDate = new Date(`${year}-06-20`);
  return addDays(firstDate, 6 - (firstDate.getDay() % 7));
};

/**
 * @author https://gist.github.com/johndyer/0dffbdd98c2046f41180c051f378f343
 * Calculates Easter in the Gregorian/Western (Catholic and Protestant) calendar
 * based on the algorithm by Oudin (1940) from http://www.tondering.dk/claus/cal/easter.php
 * @returns {array} [int month, int day]
 */
function getEaster(year: number) {
  var f = Math.floor,
    // Golden Number - 1
    G = year % 19,
    C = f(year / 100),
    // related to Epact
    H = (C - f(C / 4) - f((8 * C + 13) / 25) + 19 * G + 15) % 30,
    // number of days from 21 March to the Paschal full moon
    I = H - f(H / 28) * (1 - f(29 / (H + 1)) * f((21 - G) / 11)),
    // weekday for the Paschal full moon
    J = (year + f(year / 4) + I + 2 - C + f(C / 4)) % 7,
    // number of days from 21 March to the Sunday on or before the Paschal full moon
    L = I - J,
    month = 3 + f((L + 40) / 44),
    day = L + 28 - 31 * f(month / 4);

  return [month, day];
}
