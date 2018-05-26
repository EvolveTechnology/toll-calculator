package calculator;

import util.Day;

/**
 * Tells if a given day is a holiday.
 */
public interface HolidaySpecification {
    /**
     * @return Undefined if day is a weekend day.
     */
    boolean isHoliday(Day day);
}
