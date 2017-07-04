package tolls;

import java.util.ArrayList;
import java.util.List;

public class TollCalculator {
    private final VehicleType vehicleType;
    private final CalendarDay day;
    private final List<TimeOfDay> passes = new ArrayList<>();

    public TollCalculator(VehicleType vehicleType, CalendarDay day) {
        this.vehicleType = vehicleType;
        this.day = day;
    }

    public void passToll(TimeOfDay time) {
        passes.add(time);
    }

    public int getTollFee() {
        if (vehicleType.isTollFree()) return 0;

        TimeOfDay startOfTheHour = new TimeOfDay(0, 0);
        int totalFee = 0;
        int previousFee = 0;
        for (TimeOfDay timeOfDay : passes) {
            if (day.isTollFree())
                continue;

            int nextFee = timeOfDay.getFee();
            if (nextFee == 0) continue;

            if (isSameHour(startOfTheHour, timeOfDay)) {
                if (nextFee >= previousFee)
                    totalFee += nextFee - previousFee;
                previousFee = nextFee;
            } else {
                startOfTheHour = timeOfDay;
                totalFee += nextFee;
                previousFee = nextFee;
            }
        }

        return Math.min(totalFee, 60);
    }

    private boolean isSameHour(TimeOfDay start, TimeOfDay time) {
        return (time.hour - start.hour) * 60 + time.minute - start.minute <= 60;
    }
}
