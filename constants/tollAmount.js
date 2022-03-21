const tollAmount = require('../tollAmount');

var tollAmountByHours = {
   
    6: Object.assign({}, ...tollAmount.setTollForHalfHour(8), ...tollAmount.setTollForHalfHour(13, 30)),
    7: Object.assign({}, ...tollAmount.setTollForOneHour(18)),
    8: Object.assign({}, ...tollAmount.setTollForHalfHour(13), ...tollAmount.setTollForHalfHour(8, 30)),
    9: Object.assign({}, ...tollAmount.setTollForHalfHour(8, 30)),
    10: Object.assign({}, ...tollAmount.setTollForHalfHour(8, 30)),
    11: Object.assign({}, ...tollAmount.setTollForHalfHour(8, 30)),
    12: Object.assign({}, ...tollAmount.setTollForHalfHour(8, 30)),
    13: Object.assign({}, ...tollAmount.setTollForHalfHour(8, 30)),
    14: Object.assign({}, ...tollAmount.setTollForHalfHour(8, 30)),
    15: Object.assign({}, ...tollAmount.setTollForHalfHour(13), ...tollAmount.setTollForHalfHour(18, 30)),
    16: Object.assign({}, ...tollAmount.setTollForOneHour(18)),
    17: Object.assign({}, ...tollAmount.setTollForOneHour(13)),
    18: Object.assign({}, ...tollAmount.setTollForHalfHour(8), ...tollAmount.setTollForHalfHour(0, 30)),
};

module.exports = Object.freeze(tollAmountByHours);  