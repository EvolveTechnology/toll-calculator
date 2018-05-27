package calculator;

import util.Day;
import util.Precondition;

import java.util.*;
import java.util.concurrent.TimeUnit;
import java.util.stream.Collectors;

public class TollCalculator {

    private final Configuration configuration;

    public TollCalculator(Configuration configuration)
    {
        Precondition.isNotNull(configuration, "configuration");

        this.configuration = configuration.clone();
    }

    public TollCalculator()
    {
        this(Configuration.newDefault());
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day in ascending order
     * @return - the total toll fee for that day
     */
    public int getTollFee(Vehicle vehicle, Date... dates)
    {
        int unlimitedFee = getUnlimitedFee(vehicle, dates);
        return Integer.min(unlimitedFee, configuration.maxFeePerDay);
    }

    public int getTollFee(Date date, Vehicle vehicle)
    {
        if (isTollFreeVehicle(vehicle)) return 0;

        Calendar dateTime = dateTimeOf(date);

        if (isTollFreeDate(dateTime)) return 0;

        return getFeeForTimeOfDay(dateTime);
    }

    private int getUnlimitedFee(Vehicle vehicle, Date... dates)
    {
        List<Date> datesWithFee = Arrays.stream(dates)
                                        .filter(d -> getTollFee(d, vehicle) > 0)
                                        .collect(Collectors.toList());
        int sum = 0;

        while (!datesWithFee.isEmpty()) {
            Date firstDate = datesWithFee.remove(0);
            sum += getTollFee(firstDate, vehicle);
            skipDatesWithinChargeInterval(firstDate.getTime(), datesWithFee);
        }
        return sum;
    }

    private void skipDatesWithinChargeInterval(long intervalStartMillis, List<Date> remainingDates)
    {
        while (!remainingDates.isEmpty() && isWithinSameChargeInterval(intervalStartMillis, remainingDates.get(0))) {
            remainingDates.remove(0);
        }
    }

    private boolean isWithinSameChargeInterval(long intervalStartMillis, Date date)
    {
        long diffInMillis = date.getTime() - intervalStartMillis;
        long diffInMinutes = TimeUnit.MINUTES.convert(diffInMillis, TimeUnit.MILLISECONDS);

        return diffInMinutes <= configuration.minNumMinutesBetweenCharges;
    }

    private int getFeeForTimeOfDay(Calendar dateTime)
    {
        int hour = dateTime.get(Calendar.HOUR_OF_DAY);
        int minute = dateTime.get(Calendar.MINUTE);
        return configuration.feeForTimeOfDay.feeFor(hour, minute);
    }

    private boolean isTollFreeVehicle(Vehicle vehicle)
    {
        return configuration.isTollFreeVehicle.test(vehicle);
    }

    private boolean isTollFreeDate(Calendar dateTime)
    {
        return isWeekend(dateTime) || configuration.isHoliday.test(new Day(dateTime));
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
