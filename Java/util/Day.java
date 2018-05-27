package util;

import java.util.Calendar;

public final class Day {
    public final int year;
    public final int month;
    public final int dayOfMonth;

    /**
     * @param year       Corresponds to {@link java.util.Calendar#YEAR}
     * @param month      Corresponds to {@link java.util.Calendar#MONTH}
     * @param dayOfMonth Corresponds to {@link java.util.Calendar#DAY_OF_MONTH}
     */
    public Day(int year,
               int month,
               int dayOfMonth)
    {
        this.year = year;
        this.month = month;
        this.dayOfMonth = dayOfMonth;
    }

    public Day(Calendar calendar)
    {
        this(calendar.get(Calendar.YEAR),
             calendar.get(Calendar.MONTH),
             calendar.get(Calendar.DAY_OF_MONTH));
    }
}
