package test_utils;

import util.Day;
import util.TimeOfDay;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

public class DateTestDataBuilder {


    public static final Day
            A_SATURDAY = new Day(2018, Calendar.MAY, 19),
            A_SUNDAY = new Day(2018, Calendar.MAY, 20);

    int year;
    int month;
    int dayOfMonth;
    int hourOfDay;
    int minute;
    int second;

    public DateTestDataBuilder(int year, int month, int dayOfMonth, int hourOfDay, int minute, int second) {
        this.year = year;
        this.month = month;
        this.dayOfMonth = dayOfMonth;
        this.hourOfDay = hourOfDay;
        this.minute = minute;
        this.second = second;
    }

    public static Date timeOf(int year,
                              int month,
                              int dayOfMonth,
                              int hourOfDay,
                              int minute,
                              int second) {
        return new DateTestDataBuilder(year, month, dayOfMonth, hourOfDay, minute, second).build();
    }

    public static Date timeOf(Day day,
                              int hourOfDay,
                              int minute,
                              int second) {
        return new DateTestDataBuilder(day.year, day.month, day.dayOfMonth,
                hourOfDay, minute, second).build();
    }

    public static DateTestDataBuilder ofDay(int year,
                                            int month,
                                            int date) {
        return new DateTestDataBuilder(year, month, date, 0, 0, 0);
    }

    public DateTestDataBuilder withDay(int year,
                                       int month,
                                       int dayOfMonth) {
        this.year = year;
        this.month = month;
        this.dayOfMonth = dayOfMonth;
        return this;
    }

    public DateTestDataBuilder withDay(Day day) {
        return this.withDay(day.year, day.month, day.dayOfMonth);
    }


    public DateTestDataBuilder withTime(int hourOfDay,
                                        int minute,
                                        int second) {
        this.hourOfDay = hourOfDay;
        this.minute = minute;
        this.second = second;
        return this;
    }

    public DateTestDataBuilder withTime(TimeOfDay timeOfDay) {
        return withTime(timeOfDay.hour, timeOfDay.minute, timeOfDay.second);
    }

    public static Date aSaturday() {
        return timeOf(2018, Calendar.MAY, 19, 10, 20, 30);
    }

    public static Date aSunday() {
        return timeOf(2018, Calendar.MAY, 20, 10, 20, 30);
    }

    public Date build() {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.clear();
        calendar.set(year, month, dayOfMonth, hourOfDay, minute, second);
        return calendar.getTime();
    }
}
