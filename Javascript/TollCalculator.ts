import Vehicle from './Vehicle';

export default class TollCalculator {

    /**
    * Calculate the total toll fee for one day
    *
    * @param vehicle - the vehicle
    * @param timeStamps   - date and time of all passes on one day
    * @return - the total toll fee for that day
    */
    getDayTollFee(vehicle: Vehicle, timeStamps: Date[]): number {

        if (this.IsTollFreeDate(timeStamps[0]) || this.IsTollFreeVehicle(vehicle)) return 0;

        // Asserting dates are already sorted
        let prevTollableTimeStamp: Date;
        const tollableTimeStamps: Date[] = timeStamps.filter((ts, i) => {
            if (i === 0 || !this.timeStampsTooClose(prevTollableTimeStamp, ts)) {
                prevTollableTimeStamp = ts;
                return true;
            }
            return false;
        });

        const summarizer = (total: number, timeStamp: Date): number => total + this.getTollFee(vehicle, timeStamp);
        return Math.min(tollableTimeStamps.reduce(summarizer, 0), 60);
    }

    /**
    * Calculate the toll fee for one timestamp
    *
    * @param vehicle - the vehicle
    * @param timeStamp   - timestamp
    * @return - the toll fee
    */
    getTollFee(vehicle: Vehicle, timeStamp: Date): number {

        if (this.IsTollFreeDate(timeStamp) || this.IsTollFreeVehicle(vehicle)) return 0;
        
        let hour: number = timeStamp.getHours();
        let minute: number = timeStamp.getMinutes();

        if (hour === 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour === 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour === 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour === 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour === 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour === 15 && minute >= 0 || hour === 16 && minute <= 59) return 18;
        else if (hour === 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour === 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private IsTollFreeDate(timeStamp: Date): boolean {

        // Weekend check, day of week is 0 indexed
        if (timeStamp.getDay() > 4) return true;

        // Month is adjusted from 0 to 1 indexed for readability
        let month: number = timeStamp.getMonth() + 1; 
        let day: number = timeStamp.getDate();

        // TODO: Add year specific dates for rest of special holidays 
        if (month === 1 && (day === 1 || day === 6) ||
            month === 5 && day === 1 ||
            month === 6 && day === 6 ||
            month === 12 && (day === 24 || day === 25 || day === 26 || day === 31))
        {
            return true;
        }

        return false;
    }

    private tollFreeVehicles: [string] = ['Motorbike'];
    private IsTollFreeVehicle(vehicle: Vehicle): boolean {

        return Boolean(this.tollFreeVehicles.filter(_vehicle => _vehicle === vehicle.getType()).length);
    }

    private timeStampsTooClose(ts1: Date, ts2: Date): boolean {
        const hourInMs: number = 60 * 60 * 1000;
        return Math.abs(ts1.getTime() - ts2.getTime()) < hourInMs;
    }
}