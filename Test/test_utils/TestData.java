package test_utils;

import calculator.FeeForTimeOfDaySpecification;
import calculator.Vehicle;
import calculator.vehicles.Car;
import calculator.vehicles.Motorbike;
import util.Day;
import util.TimeOfDay;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.function.Predicate;

public class TestData {

    public static final Vehicle
            A_NON_FREE_VEHICLE = new Car(),
            A_FREE_VEHICLE = new Motorbike(),
            RANDOM_VEHICLE = new Motorbike();

    public static final Day
            MONDAY = new Day(2018, Calendar.MAY, 7),
            TUESDAY = new Day(2018, Calendar.MAY, 8),
            WEDNESDAY = new Day(2018, Calendar.MAY, 9),
            THURSDAY = new Day(2018, Calendar.MAY, 10),
            FRIDAY = new Day(2018, Calendar.MAY, 11),
            SATURDAY = new Day(2018, Calendar.MAY, 12),
            SUNDAY = new Day(2018, Calendar.MAY, 13),
            DAY_WITH_FEE = new Day(2013, Calendar.JANUARY, 2),
            HOLIDAY_DAY = new Day(2013, Calendar.JANUARY, 1);

    public static final List<NameAndValue<Day>> NON_WEEKEND_DAYS = new ArrayList<NameAndValue<Day>>() {{
        add(new NameAndValue<Day>("MONDAY", MONDAY));
        add(new NameAndValue<Day>("TUESDAY", TUESDAY));
        add(new NameAndValue<Day>("WEDNESDAY", WEDNESDAY));
        add(new NameAndValue<Day>("THURSDAY", THURSDAY));
        add(new NameAndValue<Day>("FRIDAY", FRIDAY));
    }};

    public static final List<NameAndValue<Day>> WEEKEND_DAYS = new ArrayList<NameAndValue<Day>>() {{
        add(new NameAndValue<Day>("SATURDAY", SATURDAY));
        add(new NameAndValue<Day>("SUNDAY", SUNDAY));
    }};

    public static final TimeOfDay
            NOON = new TimeOfDay(12, 0, 0),
            FEE_IS_8 = new TimeOfDay(6, 15, 0),
            FEE_IS_18 = new TimeOfDay(7, 30, 10),
            FEE_IS_0 = new TimeOfDay(19, 0, 0);

    public static FeeForTimeOfDaySpecification constantFeeOf(int fee)
    {
        return (hour, minute) -> fee;
    }

    public static FeeForTimeOfDaySpecification feeIsSameAsMinute()
    {
        return (hour, minute) -> minute;
    }

    public static FeeForTimeOfDaySpecification feeIsSameAsMinuteWhenMinuteIsEvenElseZero()
    {
        return (hour, minute) -> minute % 2 == 0 ? minute : 0;
    }

    public static Predicate<Day> holidayIsConstant(boolean isHoliday)
    {
        return day -> isHoliday;
    }

    public static Predicate<Vehicle> vehicleIsTollFreeIsConstant(boolean isTollFree)
    {
        return vehicle -> isTollFree;
    }
}
