package tolls;

import java.util.*;
import java.util.concurrent.*;

public class TollCalculator {

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTollFee(VehicleType vehicle, Date... dates) {
        if (dates.length == 0) return 0;

        Date startOfTheHour = dates[0]; // assumed to be sorted
        int totalFee = 0;
        int previousFee = 0;
        for (Date date : dates) {
            int nextFee = getTollFee(date, vehicle);

            if (isSameHour(startOfTheHour, date)) {
                if (nextFee >= previousFee)
                    totalFee += nextFee - previousFee;
                previousFee = nextFee;
            } else {
                startOfTheHour = date;
                totalFee += nextFee;
                previousFee = 0;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private boolean isSameHour(Date startOfTheHour, Date date) {
        TimeUnit timeUnit = TimeUnit.MINUTES;
        long diffInMillies = date.getTime() - startOfTheHour.getTime();
        long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);
        return minutes <= 60;
    }

    public int getTollFee(final Date date, VehicleType vehicle) {
        if (new CalendarDay(date).isTollFree(date) || vehicle.isTollFree()) return 0;

        TimeOfDay timeOfDay = new TimeOfDay(date);
        return timeOfDay.getFee();
    }
}
