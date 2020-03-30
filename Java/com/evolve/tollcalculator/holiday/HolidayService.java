package com.evolve.tollcalculator.holiday;

import java.time.LocalDate;

/**
 * The point of this interface is that we could potentially have different implementations
 * for getting holiday information. Depending on country etc. for example.
 */
public interface HolidayService {
    boolean isHoliday(final LocalDate localDate);
}
