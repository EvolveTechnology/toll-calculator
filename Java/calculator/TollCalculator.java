package calculator;

import util.Day;
import util.Precondition;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.concurrent.TimeUnit;

public class TollCalculator {

    private final Specifications specifications;

    public TollCalculator(Specifications specifications)
    {
        Precondition.isNotNull(specifications, "specifications");

        this.specifications = specifications;
    }

    public TollCalculator()
    {
        this(Specifications.newDefault());
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTollFee(Vehicle vehicle, Date... dates)
    {
        Date intervalStart = dates[0];
        int totalFee = 0;
        for (Date date : dates) {
            int nextFee = getTollFee(date, vehicle);
            int tempFee = getTollFee(intervalStart, vehicle);

            TimeUnit timeUnit = TimeUnit.MINUTES;
            long diffInMillies = date.getTime() - intervalStart.getTime();
            long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

            if (minutes <= 60) {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            } else {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    public int getTollFee(final Date date, Vehicle vehicle)
    {
        if (isTollFreeVehicle(vehicle)) return 0;

        Calendar dateTime = dateTimeOf(date);

        if (isTollFreeDate(dateTime)) return 0;

        return getFeeForTimeOfDay(dateTime);
    }

    private int getFeeForTimeOfDay(Calendar dateTime)
    {
        int hour = dateTime.get(Calendar.HOUR_OF_DAY);
        int minute = dateTime.get(Calendar.MINUTE);
        return specifications.feeForTimeOfDay.feeFor(hour, minute);
    }

    private boolean isTollFreeVehicle(Vehicle vehicle)
    {
        return specifications.isTollFreeVehicle.test(vehicle);
    }

    private boolean isTollFreeDate(Calendar dateTime)
    {
        return isWeekend(dateTime) || specifications.isHoliday.test(new Day(dateTime));
    }

    private boolean isWeekend(Calendar dateTime)
    {
        int dayOfWeek = dateTime.get(Calendar.DAY_OF_WEEK);
        return dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY;
    }

    private static Calendar dateTimeOf(Date date)
    {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        return calendar;
    }

}
