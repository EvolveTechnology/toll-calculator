// Mock Java's gregorian calendar implementation
const utcWeekend = [0, 6];

// Original Java specs
export default (date, holidays) => {
  const isWeekend = utcWeekend.includes(date.getUTCDay());
  const holidayDate = date.toISOString().split('T')[0];
  const isHoliday = holidays.includes(holidayDate);

  if (isWeekend || isHoliday) return true;

  return false;
};
