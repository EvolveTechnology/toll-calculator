const moment = require('moment');

/*
* @param {dates} dates of indifferent order
* @returns {number} the sorted dates in ascending order
*/
const sortDatesByAscOrder = (dates) => {
    return dates.sort((a, b) => {
        return moment(a).diff(b);
    })
}

/*
* @param {date1} date
* @param {date2} date
* @returns {number} the difference in minutes between the dates
*/
const getMinutesDifferenceBetweenDates = (date1, date2) => {
    return moment(date1, "DD/MM/YYYY HH:mm:ss").diff(
        moment(date2, "DD/MM/YYYY HH:mm:ss")
    ) / 60000;
}



module.exports = {
    sortDatesByAscOrder: sortDatesByAscOrder,
    getMinutesDifferenceBetweenDates: getMinutesDifferenceBetweenDates
}