import { Vehicle } from './Models';

export class TollCalculator {

    /**
     * Calculate the total toll fee for one day
     *
     * @param {Vehicle} vehicle - The vehicle
     * @param {Date[]}  dates   - Date and time of all passes on one day
     * @return {number} The total toll fee for that day
     */
    tollFee(vehicle: Vehicle, dates: Date[]): number {
        return -1;
    }
}
