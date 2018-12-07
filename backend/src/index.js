import feeByTimeOfDay from './services/feeByTimeOfDay';
import isTollFreeDate from './services/tollFreeDays';

const tz = new Date().getTimezoneOffset();
const now = new Date(new Date().getTime() - tz * 60 * 1000);

console.log('The time is: ', now);
console.log('Fee:', feeByTimeOfDay(now));
console.log('Is it a fee free day?', isTollFreeDate(now));
