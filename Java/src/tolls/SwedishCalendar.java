package tolls;

import java.util.Calendar;

// TODO: I don't know how to find all the correct date for holidays.
public class SwedishCalendar implements HolidayCalendar {
    public boolean isHoliday(int year, int month, int day) {
        if (year == 2013) {
            if (month == Calendar.MARCH && (day == 28 || day == 29) ||
                    month == Calendar.MAY && (day == 8 || day == 9) ||
                    month == Calendar.JUNE && (day == 21)) {
                return true;
            }
        } else if (year == 2017) {
            if (month == Calendar.APRIL && (day == 13 || day == 14) ||
                    month == Calendar.MAY && (day == 24 || day == 25) ||
                    month == Calendar.JUNE && (day == 23)) {
                return true;
            }
        }
        return false;
    }
}
