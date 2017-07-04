package tolls;

import org.jetbrains.annotations.NotNull;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

// I want to call this "Date," but don't want conflicts with java.util.Date
class CalendarDay {
    final int year;
    final int month; // One of the Calendar.MONTH_NAME constants
    final int day;
    final int weekday;

    CalendarDay(Date date) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTime(date);
        this.year = calendar.get(Calendar.YEAR);
        this.month = calendar.get(Calendar.MONTH);
        this.day = calendar.get(Calendar.DAY_OF_MONTH);
        this.weekday = calendar.get(Calendar.DAY_OF_WEEK);
    }

    @NotNull
    Boolean isTollFree() {
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
