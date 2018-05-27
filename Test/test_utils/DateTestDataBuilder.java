package test_utils;

import util.Day;
import util.TimeOfDay;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

public class DateTestDataBuilder {


    int year;
    int month;
    int dayOfMonth;
    int hourOfDay;
    int minute;
    int second;

    public DateTestDataBuilder(int year, int month, int dayOfMonth, int hourOfDay, int minute, int second)
    {
        this.year = year;
        this.month = month;
        this.dayOfMonth = dayOfMonth;
        this.hourOfDay = hourOfDay;
        this.minute = minute;
        this.second = second;
    }

    public DateTestDataBuilder(Day day, TimeOfDay timeOfDay)
    {
        this(day.year, day.month, day.dayOfMonth,
             timeOfDay.hour, timeOfDay.minute, timeOfDay.second);
    }

    public DateTestDataBuilder(Day day)
    {
        this(day, TimeOfDay.midnight());
    }

    public static DateTestDataBuilder ofDay(int year,
                                            int month,
                                            int date)
    {
        return new DateTestDataBuilder(year, month, date, 0, 0, 0);
    }

    public DateTestDataBuilder withDay(int year,
                                       int month,
                                       int dayOfMonth)
    {
        this.year = year;
        this.month = month;
        this.dayOfMonth = dayOfMonth;
        return this;
    }

    public DateTestDataBuilder withDay(Day day)
    {
        return this.withDay(day.year, day.month, day.dayOfMonth);
    }


    public DateTestDataBuilder withTime(int hourOfDay,
                                        int minute,
                                        int second)
    {
        this.hourOfDay = hourOfDay;
        this.minute = minute;
        this.second = second;
        return this;
    }

    public DateTestDataBuilder withMinute(int minute)
    {
        this.minute = minute;
        return this;
    }


    public DateTestDataBuilder withNextHour()
    {
        this.hourOfDay += 1;
        return this;
    }


    public DateTestDataBuilder withTime(TimeOfDay timeOfDay)
    {
        return withTime(timeOfDay.hour, timeOfDay.minute, timeOfDay.second);
    }

    public Date buildForTime(int hourOfDay,
                             int minute)
    {
        return withTime(hourOfDay, minute, second).build();
    }

    public Date build()
    {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.clear();
        calendar.set(year, month, dayOfMonth, hourOfDay, minute, second);
        return calendar.getTime();
    }
}
