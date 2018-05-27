package test_utils;

import calculator.FeeForTimeOfDaySpecification;
import calculator.Vehicle;
import calculator.Vehicles;
import util.Day;
import util.TimeOfDay;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.function.Predicate;

public class TestData {

    public static final Vehicle RANDOM_VEHICLE = Vehicles.newMotorbike();

    public static final Day
            MONDAY = new Day(2018, Calendar.MAY, 7),
            TUESDAY = new Day(2018, Calendar.MAY, 8),
            WEDNESDAY = new Day(2018, Calendar.MAY, 9),
            THURSDAY = new Day(2018, Calendar.MAY, 10),
            FRIDAY = new Day(2018, Calendar.MAY, 11),
            SATURDAY = new Day(2018, Calendar.MAY, 12),
            SUNDAY = new Day(2018, Calendar.MAY, 13);

    public static final List<NameAndValue<Day>> NON_WEEKEND_DAYS =
            new ArrayList<NameAndValue<Day>>() {{
                add(new NameAndValue<>("MONDAY", MONDAY));
                add(new NameAndValue<>("TUESDAY", TUESDAY));
                add(new NameAndValue<>("WEDNESDAY", WEDNESDAY));
                add(new NameAndValue<>("THURSDAY", THURSDAY));
                add(new NameAndValue<>("FRIDAY", FRIDAY));
            }};

    public static final TimeOfDay
            NOON = new TimeOfDay(12, 0, 0);

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
