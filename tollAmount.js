
const setTollForOneHour = (tollAmount) => {
    return Array.from(Array(60))
        .map((_, index) => index)
        .map((minute) => ({ [minute]: tollAmount }));
};


const setTollForHalfHour = (tollAmount, offset = 0) => {
    return Array.from(Array(30))
        .map((_, index) => index + offset)
        .map((minute) => ({ [minute]: tollAmount }));
};


module.exports = {
    setTollForOneHour: setTollForOneHour,
    setTollForHalfHour: setTollForHalfHour
} 