package tolls;

import java.util.Calendar;
import java.util.GregorianCalendar;

// I want to call this "Date," but don't want conflicts with java.util.Date
class CalendarDay {
    private final int year;
    private final int month; // One of the Calendar.MONTH_NAME constants
    private final int day;
    private final int weekday;

    /**
     * @param year   The year
     * @param month  One of the Calendar.MONTH_NAME constants
     * @param day    The number of the day in the month
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
        if (weekday == Calendar.SATURDAY || weekday == Calendar.SUNDAY) return true;

        if (year == 2013) {
            if (month == Calendar.JANUARY && day == 1 ||
                    month == Calendar.MARCH && (day == 28 || day == 29) ||
                    month == Calendar.APRIL && (day == 1 || day == 30) ||
                    month == Calendar.MAY && (day == 1 || day == 8 || day == 9) ||
                    month == Calendar.JUNE && (day == 5 || day == 6 || day == 21) ||
                    month == Calendar.JULY ||
                    month == Calendar.NOVEMBER && day == 1 ||
                    month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31)) {
                return true;
            }
        }
        return false;
    }
}
