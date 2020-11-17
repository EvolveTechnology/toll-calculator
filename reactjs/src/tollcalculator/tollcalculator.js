export const getFeeForTime = (time) => {
  const hour = parseInt(time.split(":")[0]);
  const minute = parseInt(time.split(":")[1]);

  if (hour === 6 && minute >= 0 && minute <= 29) return 8;
  else if (hour === 6 && minute >= 30 && minute <= 59) return 13;
  else if (hour === 7 && minute >= 0 && minute <= 59) return 18;
  else if (hour === 8 && minute >= 0 && minute <= 29) return 13;
  else if (hour >= 8 && hour <= 14 && minute >= 0 && minute <= 30) return 8;
  else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
  else if (hour === 15 && minute >= 0 && minute <= 29) return 13;
  else if ((hour === 15 && minute >= 30) || (hour === 16 && minute <= 59))
    return 18;
  else if (hour === 17 && minute >= 0 && minute <= 59) return 13;
  else if (hour === 18 && minute >= 0 && minute <= 29) return 8;
  else return 0;
};
