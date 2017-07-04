package tolls;

import java.util.*;
import java.util.concurrent.*;

public class TollCalculator {
    private final VehicleType vehicleType;
    private final List<Date> tollDates = new ArrayList<>();

    public TollCalculator(VehicleType vehicleType) {
        this.vehicleType = vehicleType;
    }

    public void passToll(Date date) {
        tollDates.add(date);
    }

    public int getTollFee() {
        Date startOfTheHour = new Date(0);
        int totalFee = 0;
        int previousFee = 0;
        for (Date date : tollDates) {
            int nextFee = getTollFee(date);
            if (nextFee == 0) continue;

            if (isSameHour(startOfTheHour, date)) {
                if (nextFee >= previousFee)
                    totalFee += nextFee - previousFee;
                previousFee = nextFee;
            } else {
                startOfTheHour = date;
                totalFee += nextFee;
                previousFee = nextFee;
            }
        }

        return Math.min(totalFee, 60);
    }

    private boolean isSameHour(Date startOfTheHour, Date date) {
        TimeUnit timeUnit = TimeUnit.MINUTES;
        long diffInMilliseconds = date.getTime() - startOfTheHour.getTime();
        long minutes = timeUnit.convert(diffInMilliseconds, TimeUnit.MILLISECONDS);
        return minutes <= 60;
    }

    private int getTollFee(final Date date) {
        if (new CalendarDay(date).isTollFree(date) || vehicleType.isTollFree()) return 0;

        TimeOfDay timeOfDay = new TimeOfDay(date);
        return timeOfDay.getFee();
    }
}
