package com.evolve.services;

import java.time.LocalDate;

public interface HolidayService {
    /**
     * Check if a date is holiday or not.
     * @param date  the date
     * @return  true if the date is a holiday, false otherwise
     */
    boolean isHoliday(LocalDate date);

    /**
     * Add a date as a holiday
     * @param date  the date
     * @return  true if the date was not a holiday, false otherwise
     */
    boolean addHoliday(LocalDate date);

    /**
     * Remove a date from the holidays.
     * @param date  the date
     * @return  true if the date was a holiday, false otherwise
     */
    boolean removeHoliday(LocalDate date);
}
