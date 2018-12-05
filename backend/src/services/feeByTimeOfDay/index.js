// Original Java specs
export default (date) => {
  const hour = date.getUTCHours();
  const minute = date.getUTCMinutes();

  if (hour === 6 && minute >= 0 && minute <= 29) return 8;
  if (hour === 6 && minute >= 30 && minute <= 59) return 13;
  if (hour === 7 && minute >= 0 && minute <= 59) return 18;
  if (hour === 8 && minute >= 0 && minute <= 29) return 13;
  if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
  if (hour === 15 && minute >= 0 && minute <= 29) return 13;
  if ((hour === 15 && minute >= 0) || (hour === 16 && minute <= 59)) return 18;
  if (hour === 17 && minute >= 0 && minute <= 59) return 13;
  if (hour === 18 && minute >= 0 && minute <= 29) return 8;
  return 0;
};
