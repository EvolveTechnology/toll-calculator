import { GetNonWeekendHolidays } from './Holidays.mjs';

const TollFreeVehicles = [
    'Motorbike',
    'Tractor',
    'Emergency',
    'Diplomat',
    'Foreign',
    'Military',
];

const EqualDate = (d1, d2) => (
    d1.getFullYear() == d2.getFullYear() &&
    d1.getMonth() == d2.getMonth() &&
    d1.getDate() == d2.getDate()
);

const IsTollFreeDate = date => {
    const year = date.getFullYear();
    const weekday = date.getDay();
    
    // Check for Saturday and Sunday
    if (weekday == 0 || weekday == 6) return true;
    const holidays = GetNonWeekendHolidays(year);
    
    return holidays.some(d => EqualDate(d, date));
}

const IsTollFreeVehicle = vehicle => TollFreeVehicles.includes(vehicle.type);

export const GetSingleTollFee = (vehicle, date) => {
    /**
     * Calculate the toll fee for one date
     *
     * @param vehicle - a vehicle with a property named "type"
     * @param dates   - date and time of one pass
     * @return - the toll fee for that pass
     */

    if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

    const hour = date.getHours();
    const minute = date.getMinutes();

    switch (true) {
        case (hour == 6):
            return minute <= 29 ? 0 : 8;
        case (hour == 7):
            return 13;
        case (hour == 8):
            return minute <= 29 ? 13 : 8;
        case (hour >= 9 && hour <= 14):
            return 8;
        case (hour == 15):
            return minute <= 29 ? 13 : 18;
        case (hour == 16):
            return 18;
        case (hour == 17):
            return 13;
        case (hour == 18):
            return minute <= 29 ? 8 : 0;
        default:
            return 0;
    }
}

export const GetTotalTollFee = (vehicle, dates) => {
    /**
     * Calculate the total toll fee for a series of datetime objects and a vehicle
     *
     * @param vehicle - a vehicle with a property named "type"
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    const intervalStart = dates?.[0];
    let startOfHour = new Date(0);
    let totalFee = 0;
    let tempFee = 0;

    dates?.forEach(date => {
        let currFee = GetSingleTollFee(vehicle, date);

        const secondsDelta = Math.abs(date - startOfHour);
        const hoursDelta = secondsDelta / (60 * 60);

        if (hoursDelta <= 1) {
            if (totalFee > 0) totalFee -= tempFee;
            // Save the highest fee as tempFee
            tempFee = Math.max(tempFee, currFee);
            totalFee += tempFee;
        }

        if (hoursDelta > 1) {
            // New hour
            startOfHour = date;
            totalFee += currFee;
            tempFee = currFee;
        }
    });

    return Math.min(totalFee, 60);
}