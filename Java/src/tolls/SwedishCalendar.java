package tolls;

import java.util.Calendar;
import java.util.GregorianCalendar;

public class SwedishCalendar implements HolidayCalendar {
    public CalendarDay easterDay(int year) {
        // Easter occurs on the first Sunday after the first full moon
        // after the spring equinox.
        // Algorithm by Carl Friedrich Gauss
        // https://sv.wikipedia.org/wiki/Påskdagen

        int a = year % 19;
        int b = year % 4;
        int c = year % 7;
        int d = (19 * a + 24) % 30;
        int e = (2 * b + 4 * c + 6 * d + 5) % 7;

        if (d + e > 9) {
            int easterDay = d + e - 9;
            if (easterDay == 26) easterDay = 19;
            if (easterDay == 25 && d == 28 && e == 6) easterDay = 18;
            return new CalendarDay(year, Calendar.APRIL, easterDay, this);
        } else {
            return new CalendarDay(year, Calendar.MARCH, 22 + d + e, this);
        }
    }

    public boolean isMidsummerEve(CalendarDay calendarDay) {
        // According to Wikipedia, Swedish midsummer eve is always
        // celebrated on the Friday that occurs between 19-25 of June.
        if (calendarDay.month != Calendar.JUNE || calendarDay.day < 19 || calendarDay.day > 25) {
            return false;
        }

        Calendar calendar = GregorianCalendar.getInstance();
        calendar.set(calendarDay.year, calendarDay.month, calendarDay.day);
        int weekday = calendar.get(Calendar.DAY_OF_WEEK);
        return weekday == Calendar.FRIDAY;
    }
}
