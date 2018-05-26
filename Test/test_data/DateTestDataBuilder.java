package test_data;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

public class DateTestDataBuilder {
    int year;
    int month;
    int date;
    int hourOfDay;
    int minute;
    int second;

    public DateTestDataBuilder(int year, int month, int date, int hourOfDay, int minute, int second) {
        this.year = year;
        this.month = month;
        this.date = date;
        this.hourOfDay = hourOfDay;
        this.minute = minute;
        this.second = second;
    }

    public static Date timeOf(int year,
                              int month,
                              int date,
                              int hourOfDay,
                              int minute,
                              int second) {
        return new DateTestDataBuilder(year, month, date, hourOfDay, minute, second).build();
    }

    public static DateTestDataBuilder ofDay(int year,
                                            int month,
                                            int date) {
        return new DateTestDataBuilder(year, month, date, 0, 0, 0);
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
        calendar.set(year, month, date, hourOfDay, minute, second);
        return calendar.getTime();
    }

    public DateTestDataBuilder withTime(int hourOfDay,
                                        int minute,
                                        int second) {
        this.hourOfDay = hourOfDay;
        this.minute = minute;
        this.second = second;
        return this;
    }
}
