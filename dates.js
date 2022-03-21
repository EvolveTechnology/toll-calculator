const moment = require('moment');


const sortDatesByAscOrder = (dates) => {
    return dates.sort((a, b) => {
        return moment(a).diff(b);
    })
}


const getMinutesDifferenceBetweenDates = (date1, date2) => {
    return moment(date1, "DD/MM/YYYY HH:mm:ss").diff(
        moment(date2, "DD/MM/YYYY HH:mm:ss")
    ) / 60000;
}



module.exports = {
    sortDatesByAscOrder: sortDatesByAscOrder,
    getMinutesDifferenceBetweenDates: getMinutesDifferenceBetweenDates
} 