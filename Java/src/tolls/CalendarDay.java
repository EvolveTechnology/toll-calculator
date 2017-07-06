package tolls;

import org.jetbrains.annotations.NotNull;

import java.util.Calendar;
import java.util.GregorianCalendar;

class CalendarDay {
    private final int year;
    private final int month; // One of the Calendar.MONTH_NAME constants
    private final int day;
    private final int weekday;

    /**
     * @param year  The year
     * @param month One of the Calendar.MONTH_NAME constants
     * @param day   The number of the day in the month
     */
    CalendarDay(int year, int month, int day) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.set(year, month, day);
        this.year = year;
        this.month = month;
        this.day = day;
        this.weekday = calendar.get(Calendar.DAY_OF_WEEK);
    }

    boolean isTollFree() {
        return isWeekend() ||
                isFixedHoliday() ||
                isMidsummerEve() ||
                isFreeForGoodFriday() ||
                isFreeForAscensionDay();
    }

    private boolean isFreeForAscensionDay() {
        CalendarDay ascensionDay = ascensionDay(year);
        return month == ascensionDay.month && (day == ascensionDay.day || day == ascensionDay.day - 1);
    }

    private boolean isFreeForGoodFriday() {
        CalendarDay easterDay = easterDay(year);
        return easterDay.month == month && (day == easterDay.day - 1 || day == easterDay.day - 2);
    }

    private boolean isWeekend() {
        return weekday == Calendar.SATURDAY || weekday == Calendar.SUNDAY;
    }

    private boolean isFixedHoliday() {
        return month == Calendar.JANUARY && day == 1 ||
                month == Calendar.APRIL && (day == 1 || day == 30) ||
                month == Calendar.MAY && day == 1 ||
                month == Calendar.JUNE && (day == 5 || day == 6) ||
                month == Calendar.JULY ||
                month == Calendar.NOVEMBER && day == 1 ||
                month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31);
    }

    @NotNull
    private CalendarDay ascensionDay(int year) {
        // The Ascension of Christ occurs on the Thursday 40 days after Easter Day.
        return easterDay(year).add39Days();
    }

    @NotNull
    private CalendarDay add39Days() {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.set(year, month, day);
        calendar.add(Calendar.DAY_OF_MONTH, 39);
        return new CalendarDay(year,
                calendar.get(Calendar.MONTH),
                calendar.get(Calendar.DAY_OF_MONTH));
    }

    @NotNull
    private CalendarDay easterDay(int year) {
        // Easter occurs on the first Sunday after the first full moon
        // after the spring equinox.
        // Algorithm by Carl Friedrich Gauss
        // https://sv.wikipedia.org/wiki/Påskdagen

        final int a = year % 19;
        final int b = year % 4;
        final int c = year % 7;
        final int d = (19 * a + 24) % 30;
        final int e = (2 * b + 4 * c + 6 * d + 5) % 7;

        if (d + e > 9) {
            int easterDay = d + e - 9;
            if (easterDay == 26) easterDay = 19;
            if (easterDay == 25 && d == 28 && e == 6) easterDay = 18;
            return new CalendarDay(year, Calendar.APRIL, easterDay);
        } else {
            return new CalendarDay(year, Calendar.MARCH, 22 + d + e);
        }
    }

    private boolean isMidsummerEve() {
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

    @Override
    @NotNull
    public String toString() {
        return year + "-" + (month + 1) + "-" + day;
    }
}
