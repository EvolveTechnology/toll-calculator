const tollAmountHelpers = require('../helpers/toll-amount')

//Setting the toll Amount Dynamically
const tollAmountByHours = {
    6: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8), ...tollAmountHelpers.setTollForHalfHour(13, 30)),
    7: Object.assign({}, ...tollAmountHelpers.setTollForOneHour(18)),
    8: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(13), ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    9: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    10: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    11: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    12: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    13: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    14: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8, 30)),
    15: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(13), ...tollAmountHelpers.setTollForHalfHour(18, 30)),
    16: Object.assign({}, ...tollAmountHelpers.setTollForOneHour(18)),
    17: Object.assign({}, ...tollAmountHelpers.setTollForOneHour(13)),
    18: Object.assign({}, ...tollAmountHelpers.setTollForHalfHour(8), ...tollAmountHelpers.setTollForHalfHour(0, 30)),
};

module.exports = Object.freeze(tollAmountByHours);
