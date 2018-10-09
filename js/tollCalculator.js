import getTimeFromISOString from './utils';
import {tollFreeVehicles} from "./vehicle";

const tolls = [
  { start: "06:00", stop: "06:29", fee: 9 },
  { start: "06:30", stop: "06:59", fee: 16 },
  { start: "07:00", stop: "07:59", fee: 22 },
  { start: "08:00", stop: "08:29", fee: 16 },
  { start: "08:30", stop: "14:59", fee: 9 },
  { start: "15:00", stop: "15:29", fee: 16 },
  { start: "15:30", stop: "16:59", fee: 22 },
  { start: "17:00", stop: "17:59", fee: 16 },
  { start: "18:00", stop: "18:29", fee: 9 },
];

const freeDates = [
  new Date('2018-01-01'),
  new Date('2018-01-05'),
  new Date('2018-03-29'),
  new Date('2018-03-30'),
  new Date('2018-04-02'),
  new Date('2018-04-30'),
  new Date('2018-05-01'),
  new Date('2018-05-09'),
  new Date('2018-05-10'),
  new Date('2018-06-05'),
  new Date('2018-06-06'),
  new Date('2018-06-22'),
  new Date('2018-11-02'),
  new Date('2018-12-24'),
  new Date('2018-12-25'),
  new Date('2018-12-26'),
  new Date('2018-12-31'),
];

const SATURDAY = 6;
const SUNDAY = 0;
const JULY = 6;

function isTollFreeVehicle(passage) {
  return tollFreeVehicles.hasOwnProperty(passage.vehicleType);
}
function isTollFreeDay(date) {
  return (date.getDay() === SUNDAY || date.getDay() === SATURDAY);
}

function isTollFreeTime(time) {
  const min = tolls[0].start,
    max = tolls[tolls.length-1].stop;

  return (time < min || time > max);
}

function isTollFreeDate(date) {
  let normalizedDate = new Date(date.toUTCString());
  normalizedDate.setUTCHours(0, 0, 0, 0);

  if (normalizedDate.getMonth() === JULY) return true;

  return freeDates.some(
    freeDate => freeDate.getTime() === normalizedDate.getTime()
  );
}

function getTollFromDate(date) {
  const time = getTimeFromISOString(date);
  let tollFee = 0;

  for (const toll of tolls) {
    if (time >= toll.start && time <= toll.stop) {
      tollFee = toll.fee;
    }
  }

  return tollFee;
}

function isTollFree(passage) {
  const date = passage.date;
  const time = getTimeFromISOString(date);
  return (
    isTollFreeVehicle(passage) ||
    isTollFreeDay(date) ||
    isTollFreeTime(time) ||
    isTollFreeDate(date)
  );
}

export {
  isTollFreeVehicle,
  getTimeFromISOString,
  getTollFromDate,
  isTollFreeDay,
  isTollFreeTime,
  isTollFreeDate,
  isTollFree,
};

