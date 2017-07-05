package tolls;

import java.util.Calendar;
import java.util.GregorianCalendar;

// I want to call this "Date," but don't want conflicts with java.util.Date
class CalendarDay {
    final int year;
    final int month; // One of the Calendar.MONTH_NAME constants
    final int day;
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

    boolean isTollFree(HolidayCalendar holidayCalendar) {
        return isWeekend() ||
                isFixedHoliday() ||
                isMovingHoliday(holidayCalendar);
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

    private boolean isMovingHoliday(HolidayCalendar holidayCalendar) {
        if (holidayCalendar.isMidsummerEve(this)) return true;

        CalendarDay easterDay = holidayCalendar.easterDay(year);
        // Both Thursday and Friday before Easter are toll-free.
        if (easterDay.month == month && (day == easterDay.day - 1 || day == easterDay.day - 2)) return true;

        // The Ascension of Christ occurs on the Thursday, 40 days after Easter.
        CalendarDay ascensionDay = easterDay.add40Days();

        // Both Wednesday and Thursday of the Ascension are toll-free.
        return month == ascensionDay.month && (day == ascensionDay.day || day == ascensionDay.day - 1);
    }

    private CalendarDay add40Days() {
        int day = this.day;
        int month;
        if (this.month == Calendar.MARCH) {
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
        return new CalendarDay(year, month, day);
    }

    @Override
    public String toString() {
        return year + "-" + (month + 1) + "-" + day;
    }
}
