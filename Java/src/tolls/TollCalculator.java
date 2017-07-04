package tolls;

import java.util.*;
import java.util.concurrent.*;

public class TollCalculator {
    private final VehicleType vehicleType;
    private final CalendarDay day;
    private final List<Date> tollDates = new ArrayList<>();

    public TollCalculator(VehicleType vehicleType, CalendarDay day) {
        this.vehicleType = vehicleType;
        this.day = day;
    }

    public void passToll(Date date) {
        tollDates.add(date);
    }

    public int getTollFee() {
        if (vehicleType.isTollFree()) return 0;

        Date startOfTheHour = new Date(0);
        int totalFee = 0;
        int previousFee = 0;
        for (Date date : tollDates) {
            if (day.isTollFree())
                continue;

            int nextFee = new TimeOfDay(date).getFee();
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

}
