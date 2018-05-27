package calculator;

import calculator.specifications.DefaultFeeForTimeOfDaySpecification;
import calculator.specifications.DefaultTollFreeVehicles;
import calculator.specifications.HolidaySpecificationFor2013;
import util.Day;
import util.Precondition;

import java.util.function.Predicate;

/**
 * Helper class to keep Toll Calculator specification components
 */
public final class Configuration implements Cloneable {

    public FeeForTimeOfDaySpecification feeForTimeOfDay;
    public Predicate<Day> isHoliday;
    public Predicate<Vehicle> isTollFreeVehicle;
    public int maxFeePerDay;
    public int minNumMinutesBetweenCharges;

    public Configuration(FeeForTimeOfDaySpecification feeForTimeOfDay,
                         Predicate<Day> isHoliday,
                         Predicate<Vehicle> isTollFreeVehicle,
                         int maxFeePerDay,
                         int minNumMinutesBetweenCharges)
    {
        Precondition.isNotNull(feeForTimeOfDay, "feeForTimeOfDay");
        Precondition.isNotNull(isHoliday, "isHoliday");
        Precondition.isNotNull(isTollFreeVehicle, "isTollFreeVehicle");

        this.feeForTimeOfDay = feeForTimeOfDay;
        this.isHoliday = isHoliday;
        this.isTollFreeVehicle = isTollFreeVehicle;
        this.maxFeePerDay = maxFeePerDay;
        this.minNumMinutesBetweenCharges = minNumMinutesBetweenCharges;
    }

    public static Configuration newDefault()
    {
        return new Configuration(new DefaultFeeForTimeOfDaySpecification(),
                                 new HolidaySpecificationFor2013(),
                                 new DefaultTollFreeVehicles(),
                                 60,
                                 60);
    }

    @Override
    public Configuration clone()
    {
        return new Configuration(feeForTimeOfDay,
                                 isHoliday,
                                 isTollFreeVehicle,
                                 maxFeePerDay,
                                 minNumMinutesBetweenCharges);
    }
}
