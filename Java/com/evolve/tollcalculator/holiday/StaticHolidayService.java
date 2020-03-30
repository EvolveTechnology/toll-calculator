package com.evolve.tollcalculator.holiday;

import java.time.LocalDate;
import java.util.List;

import static java.util.Arrays.asList;

/**
 * For this assignment I thought it would be enough to just store all holidays like this.
 * These are not the correct holidays for 2020, btw.
 */
public class StaticHolidayService implements HolidayService {
    private static List<LocalDate> HOLIDAYS = asList(
            LocalDate.of(2020, 1, 1),
            LocalDate.of(2020, 3, 28),
            LocalDate.of(2020, 3, 29),
            LocalDate.of(2020, 4, 1),
            LocalDate.of(2020, 4, 30),
            LocalDate.of(2020, 5, 1),
            LocalDate.of(2020, 5, 8),
            LocalDate.of(2020, 5, 9),
            LocalDate.of(2020, 6, 5),
            LocalDate.of(2020, 6, 6),
            LocalDate.of(2020, 6, 21),
            LocalDate.of(2020, 11, 1),
            LocalDate.of(2020, 12, 24),
            LocalDate.of(2020, 12, 25),
            LocalDate.of(2020, 12, 26),
            LocalDate.of(2020, 12, 31)
    );

    @Override
    public boolean isHoliday(final LocalDate date) {
        return HOLIDAYS.stream().anyMatch(holiday -> holiday.equals(date));
    }
}
