import { config } from "../config";
import { HOLIDAYS, ONE_HOUR, MAX_DAILY_FEE } from ".././constants/constants";

export const getTotalFeeForDate = (vehicle, date) => {
  if (isTollFreeDate(date) || isTollFreeVehicle(vehicle)) {
    return 0;
  }

  let fee = 0;
  const passages = Object.values(vehicle.passages[date]);
  const hourSlots = groupTimesByHourSlots(passages, date);

  hourSlots.forEach((slot) => {
    fee += getHighestFeeForHourSlot(slot);
  });

  if (fee > MAX_DAILY_FEE) fee = MAX_DAILY_FEE;

  return fee;
};

export const getHighestFeeForHourSlot = (hourSlot) =>
  hourSlot
    .map((time) => getFeeForTime(time))
    .reduce((fee1, fee2) => Math.max(fee1, fee2));

export const groupTimesByHourSlots = (times, date) => {
  let groupedHourSlots = [];
  let hourSlot = [];
  let firstPassageInSlot = times[0];

  times.forEach((time, index) => {
    if (isLessThanOneHourAgo(time, firstPassageInSlot, date)) {
      hourSlot.push(time);
      if (index === times.length - 1) {
        groupedHourSlots.push(hourSlot);
      }
    } else {
      groupedHourSlots.push(hourSlot);
      hourSlot = [time];
      firstPassageInSlot = time;
      if (index === times.length - 1) {
        groupedHourSlots.push(hourSlot);
      }
    }
  });

  return groupedHourSlots;
};

export const isLessThanOneHourAgo = (time, since, date) => {
  const timeToCheck = new Date(`${date}T${time}`);
  const timeToCheckFrom = new Date(`${date}T${since}`);
  const anHourAgo = timeToCheck - ONE_HOUR;

  return timeToCheckFrom > anHourAgo;
};

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

export const isTollFreeVehicle = (vehicle) => {
  return config.tollFreeVehicles.includes(vehicle.type);
};

export const isTollFreeDate = (date) => {
  const day = new Date(date).getDay();
  const isWeekend = day === 6 || day === 0;
  return isWeekend || HOLIDAYS.includes(date);
};

export const notChargedPassagesForDate = (passages, date) => {
  const hourSlots = groupTimesByHourSlots(passages, date);
  let notChargedPassages = [];

  hourSlots.forEach((slot) => {
    const highestFee = getHighestFeeForHourSlot(slot);
    const slotWithFees = slot.map((time) => {
      return { time: time, fee: getFeeForTime(time) };
    });
    const chargedPassageTime = slotWithFees.find((time) => {
      return time.fee === highestFee;
    });
    const notChargedTimes = slotWithFees
      .filter((time) => time !== chargedPassageTime)
      .map((time) => time.time);
    notChargedPassages = [...notChargedPassages, ...notChargedTimes];
  });

  return notChargedPassages;
};
