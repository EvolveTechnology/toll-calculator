package test_utils;

import calculator.Car;
import calculator.Motorbike;
import calculator.Vehicle;
import util.Day;
import util.TimeOfDay;

import java.util.Calendar;

public class TestData {

    public static final Vehicle
            A_NON_FREE_VEHICLE = new Car(),
            A_FREE_VEHICLE = new Motorbike();

    public static final Day
            DAY_WITH_FEE = new Day(2013, Calendar.JANUARY, 2),
            HOLIDAY_DAY = new Day(2013, Calendar.JANUARY, 1);


    public static final TimeOfDay
            FEE_IS_8 = new TimeOfDay(6, 15, 0),
            FEE_IS_18 = new TimeOfDay(7, 30, 10),
            FEE_IS_0 = new TimeOfDay(19, 0, 0);
}
