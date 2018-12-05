// Mock Java's gregorian calendar implementation
const Calendar = {
  JANUARY: 0,
  FEBRUARY: 1,
  MARCH: 2,
  APRIL: 3,
  MAY: 4,
  JUNE: 5,
  JULY: 6,
  AUGUST: 7,
  SEPTEMBER: 8,
  OCTOBER: 9,
  NOVEMBER: 10,
  DECEMBER: 11,
  SATURDAY: 6,
  SUNDAY: 0,
};

// Original Java specs
export default (date) => {
  const year = date.getUTCFullYear();
  const month = date.getUTCMonth();
  const day = date.getUTCDate();

  const dayOfWeek = date.getUTCDay();
  if (dayOfWeek === Calendar.SATURDAY || dayOfWeek === Calendar.SUNDAY) return true;

  if (year === 2013) {
    if (
      (month === Calendar.JANUARY && day === 1)
      || (month === Calendar.MARCH && (day === 28 || day === 29))
      || (month === Calendar.APRIL && (day === 1 || day === 30))
      || (month === Calendar.MAY && (day === 1 || day === 8 || day === 9))
      || (month === Calendar.JUNE && (day === 5 || day === 6 || day === 21))
      || month === Calendar.JULY
      || (month === Calendar.NOVEMBER && day === 1)
      || (month === Calendar.DECEMBER && (day === 24 || day === 25 || day === 26 || day === 31))
    ) {
      return true;
    }
  }
  return false;
};
