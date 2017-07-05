package tolls;

import java.util.Calendar;
import java.util.GregorianCalendar;

public class SwedishCalendar implements HolidayCalendar {
    public boolean isHoliday(CalendarDay calendarDay) {
        return isMidsummerEve(calendarDay) ||
                isDayOrEveOfAscension(calendarDay) ||
                isEasterFriday(calendarDay);
    }

    private boolean isEasterFriday(CalendarDay date) {
        CalendarDay easterDay = easterDay(date.year);
        return easterDay.month == date.month && (date.day == easterDay.day - 1 || date.day == easterDay.day - 2);

    }

    private boolean isDayOrEveOfAscension(CalendarDay calendarDay) {
        int year = calendarDay.year;
        int month = calendarDay.month;
        int day = calendarDay.day;

        // Both Wednesday and Thursday are toll-free.
        CalendarDay ascensionDay = ascensionDay(year);
        return month == ascensionDay.month && (day == ascensionDay.day || day == ascensionDay.day - 1);
    }

    private CalendarDay ascensionDay(int year) {
        // The Ascension of Christ occurs on the Thursday, 40 days after Easter.
        CalendarDay easterDay = easterDay(year);

        int day = easterDay.day;
        int month;
        if (easterDay.month == Calendar.MARCH) {
            day += 9;
            if (day <= 30) {
                month = Calendar.APRIL;
            } else {
                month = Calendar.MAY;
                day -= 30;
            }
        } else { // April
            day += 10;
            if (day <= 31) {
                month = Calendar.MAY;
            } else {
                month = Calendar.JUNE;
                day -= 31;
            }
        }
        return new CalendarDay(year, month, day, this);
    }

    private CalendarDay easterDay(int year) {
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
        int year = calendarDay.year;
        int month = calendarDay.month;
        int day = calendarDay.day;

        // According to Wikipedia, Swedish midsummer eve is always
        // celebrated on the Friday that occurs between 19-25 of June.
        if (month != Calendar.JUNE || day < 19 || day > 25) {
            return false;
        }

        Calendar calendar = GregorianCalendar.getInstance();
        calendar.set(year, month, day);
        int weekday = calendar.get(Calendar.DAY_OF_WEEK);
        return weekday == Calendar.FRIDAY;
    }
}
