import { Vehicle, VehicleType } from './Models';
import { FEE_DURATION_IN_MINUTES, FREE_DATES, FREE_DAYS, FEE_INTERVALS, FREE_VEHICLE_TYPES, MAX_FEE_PER_DAY } from './config';

export class TollCalculator {

    /**
     * Calculate the total toll fee for one day
     *
     * @param  {Vehicle} vehicle - The vehicle
     * @param  {Date[]}  dates   - Date and time of all passes on one day
     * @return {number}  The total toll fee for that day
     */
    tollFee(vehicle: Vehicle, dates: Date[]): number {
        if (this.isTollFreeVehicle(vehicle) || this.isTollFreeDay(dates)) {
            return 0;
        }

        return dates
        // Filter out dates that are within the FEE_DURATION_IN_MINUTES
        .reduce((acc, c) => {
            const minutes = this.msToMinutes(c);
            if (Math.abs(minutes - acc.currMinutes) >= FEE_DURATION_IN_MINUTES) {
                return {
                    currMinutes: minutes,
                    accepted: acc.accepted.concat(c)
                }
            }
            return acc;
        }, { currMinutes: 0, accepted: [] })
        .accepted
        // Calculate all dates
        .reduce((total, curr) => {
            // Maxumium per day reached
            if (total >= MAX_FEE_PER_DAY) {
                return MAX_FEE_PER_DAY;
            }
            return total + this.tollFeeForDate(curr);
        }, 0);
    }

    private isTollFreeVehicle(vehicle: Vehicle) {
        return FREE_VEHICLE_TYPES.some(type => type === vehicle.type);
    }

    private isTollFreeDay(dates: Date[]): boolean {
        // Assume that all dates are within the same day
        const date = dates[0];
        return date &&
               // Free days
               FREE_DAYS.some(fd => fd === date.getUTCDay()) ||
               // Free dates
               FREE_DATES.some(md => md.month === date.getUTCMonth() && md.dayOfMonth === date.getUTCDate());
    }

    private tollFeeForDate(d: Date) {
        // Calculate fee for current date
        const minutes = d.getUTCMinutes() + (d.getUTCHours()*60);
        return FEE_INTERVALS
        .filter(interval => {
            const fromMinutes = interval.from.minute + (interval.from.hour * 60);
            const toMinutes = interval.to.minute + (interval.to.hour * 60);
            return minutes >= fromMinutes && minutes < toMinutes
        })
        .map(interval => interval.fee)
        .pop() || 0;
    }

    private msToMinutes(d: Date) {
        return d.getTime()/1000/60;
    }

}
