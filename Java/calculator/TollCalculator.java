package calculator;

import util.Day;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.concurrent.TimeUnit;
import java.util.function.Predicate;

public class TollCalculator {

    private final FeeForTimeOfDaySpecification feeForTimeOfDaySpecification;
    private final HolidaySpecification holidaySpecification;
    private final Predicate<Vehicle> isTollFreeVehicle;

    public TollCalculator(FeeForTimeOfDaySpecification feeForTimeOfDaySpecification,
                          HolidaySpecification holidaySpecification,
                          Predicate<Vehicle> isTollFreeVehicle) {
        if (feeForTimeOfDaySpecification == null) {
            throw new NullPointerException("feeForTimeOfDaySpecification");
        }
        if (holidaySpecification == null) {
            throw new NullPointerException("holidaySpecification");
        }
        if (isTollFreeVehicle == null) {
            throw new NullPointerException("isTollFreeVehicle");
        }
        this.feeForTimeOfDaySpecification = feeForTimeOfDaySpecification;
        this.holidaySpecification = holidaySpecification;
        this.isTollFreeVehicle = isTollFreeVehicle;
    }

    public TollCalculator() {
        this(new DefaultFeeForTimeOfDaySpecification(),
                new HolidaySpecificationFor2013(),
                new DefaultTollFreeVehicles());
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int getTollFee(Vehicle vehicle, Date... dates) {
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

    public int getTollFee(final Date date, Vehicle vehicle) {
        if (isTollFreeDate(date) || isTollFreeVehicle(vehicle)) return 0;
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        int hour = calendar.get(Calendar.HOUR_OF_DAY);
        int minute = calendar.get(Calendar.MINUTE);

        return feeForTimeOfDaySpecification.feeFor(hour, minute);
    }

    private boolean isTollFreeVehicle(Vehicle vehicle) {
        return isTollFreeVehicle.test(vehicle);
    }

    private Boolean isTollFreeDate(Date date) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);

        int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
        if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

        return holidaySpecification.isHoliday(new Day(calendar));
    }

}
