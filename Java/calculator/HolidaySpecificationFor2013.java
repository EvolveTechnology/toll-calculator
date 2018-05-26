package calculator;

import util.Day;

import java.util.Calendar;

/**
 * Tells if a given day is a holiday.
 */
public class HolidaySpecificationFor2013 implements HolidaySpecification {
    /**
     * @return Undefined if day is a weekend day.
     */
    public boolean isHoliday(Day day) {
        if (day.year == 2013) {
            if (day.month == Calendar.JANUARY && day.dayOfMonth == 1 ||
                    day.month == Calendar.MARCH && (day.dayOfMonth == 28 || day.dayOfMonth == 29) ||
                    day.month == Calendar.APRIL && (day.dayOfMonth == 1 || day.dayOfMonth == 30) ||
                    day.month == Calendar.MAY && (day.dayOfMonth == 1 || day.dayOfMonth == 8 || day.dayOfMonth == 9) ||
                    day.month == Calendar.JUNE && (day.dayOfMonth == 5 || day.dayOfMonth == 6 || day.dayOfMonth == 21) ||
                    day.month == Calendar.JULY ||
                    day.month == Calendar.NOVEMBER && day.dayOfMonth == 1 ||
                    day.month == Calendar.DECEMBER && (day.dayOfMonth == 24 || day.dayOfMonth == 25 || day.dayOfMonth == 26 || day.dayOfMonth == 31)) {
                return true;
            }
        }
        return false;
    }
}
