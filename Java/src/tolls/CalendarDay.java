package tolls;

import java.util.Calendar;
import java.util.GregorianCalendar;

// I want to call this "Date," but don't want conflicts with java.util.Date
class CalendarDay {
    final int year;
    final int month; // One of the Calendar.MONTH_NAME constants
    final int day;
    private final int weekday;

    private final HolidayCalendar holidayCalendar;

    /**
     * @param year  The year
     * @param month One of the Calendar.MONTH_NAME constants
     * @param day   The number of the day in the month
     * @param holidayCalendar
     */
    CalendarDay(int year, int month, int day, HolidayCalendar holidayCalendar) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.set(year, month, day);
        this.year = year;
        this.month = month;
        this.day = day;
        this.weekday = calendar.get(Calendar.DAY_OF_WEEK);
        this.holidayCalendar = holidayCalendar;
    }

    boolean isTollFree() {
        return isWeekend() ||
                isFixedHoliday() ||
                isMovingHoliday();
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

    private boolean isMovingHoliday() {
        return holidayCalendar.isMidsummerEve(this) ||
                isDayOrEveOfAscension(this) ||
                isEasterFriday(this);
    }

    private boolean isEasterFriday(CalendarDay date) {
        // Both Thursday and Friday are toll-free.
        CalendarDay easterDay = holidayCalendar.easterDay(date.year);
        return easterDay.month == date.month && (date.day == easterDay.day - 1 || date.day == easterDay.day - 2);

    }

    private boolean isDayOrEveOfAscension(CalendarDay calendarDay) {
        // Both Wednesday and Thursday are toll-free.
        CalendarDay ascensionDay = ascensionDay(calendarDay.year);
        return calendarDay.month == ascensionDay.month && (calendarDay.day == ascensionDay.day || calendarDay.day == ascensionDay.day - 1);
    }

    private CalendarDay ascensionDay(int year) {
        // The Ascension of Christ occurs on the Thursday, 40 days after Easter.
        CalendarDay easterDay = holidayCalendar.easterDay(year);

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
        return new CalendarDay(year, month, day, holidayCalendar);
    }



    @Override
    public String toString() {
        return year + "-" + (month + 1) + "-" + day;
    }
}
