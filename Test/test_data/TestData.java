package test_data;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import calculator.Car;
import calculator.Motorbike;
import calculator.Vehicle;

public class TestData {
    public static Date timeOf(int year,
                              int month,
                              int date,
                              int hourOfDay,
                              int minute,
                              int second) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.clear();
        calendar.set(year, month, date, hourOfDay, minute, second);
        return calendar.getTime();
    }

    public static Date aSaturday() {
        return timeOf(2018, Calendar.MAY, 19, 10, 20, 30);
    }

    public static Date aSunday() {
        return timeOf(2018, Calendar.MAY, 20, 10, 20, 30);
    }

    public static Vehicle aNonFreeVehicle() {
        return new Car();
    }

    public static Vehicle aFreeVehicle() {
        return new Motorbike();
    }
}
