import feeByTimeOfDay from './services/feeByTimeOfDay';
import isTollFreeDate from './services/tollFreeDays';

const now = new Date();

console.log('Fee:', feeByTimeOfDay(now));
console.log('Is Fee Free?', isTollFreeDate(now));
