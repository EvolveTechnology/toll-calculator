package com.evolve.services;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.*;


/**
 * The holidays are:
 * New Year's Day       Jan. 1st
 * Epiphany             Jan. 6th
 * Walpurgis Night      Apr. 30th
 * May 1st              May  1st
 * National Day         Jun. 6th
 * Christmas Eve        Dec. 24th
 * Christmas Day        Dec. 25th
 * Boxing Day           Dec. 26th
 * New Year's Eve       Dec. 31st
 * Good Friday
 * Easter Monday
 * Ascension Day
 * Midsummer's Eve
 * All Saints' Eve
 */
public class HolidayServiceImpl implements HolidayService {
    private static Set<LocalDate> holidays;

    @Override
    public boolean isHoliday(LocalDate date) {
        return holidays.contains(date);
    }

    static {
        holidays = new HashSet<>();
        for (int year = 2020; year < 2022; ++year) {
            holidays.addAll(getFixedHolidays(year));
            holidays.add(getMidsummerEve(year));
            holidays.add(getAllSaintsEve(year));
            LocalDate easterDay = calculateEaster(year);
            // add Good Friday
            holidays.add(easterDay.minusDays(2));
            // add Easter's Eve and Easter Sunday
            holidays.add(easterDay.minusDays(1));
            holidays.add(easterDay);
            // add Easter Monday
            holidays.add(easterDay.plusDays(1));
            // 	add Ascension Day
            holidays.add(easterDay.plusDays(39));
        }
    }

    /**
     * Get the Midsummer's Eve of a given year.
     * @param year  the specified year
     * @return  the Midsummer's Eve
     */
    private static LocalDate getMidsummerEve(int year) {
        // Earliest possible date of Midsumer's Eve is Jun. 19th
        return getFirstFriday(LocalDate.of(year, 6, 19));
    }

    /**
     * Get the All Saints' Eve of a given year.
     * @param year  the specified year
     * @return  the All Saints' Eve
     */
    private static LocalDate getAllSaintsEve(int year) {
        // Earliest possible date of All Saints' Eve is Oct. 30th
        return getFirstFriday(LocalDate.of(year, 10, 30));
    }

    /**
     * Get the first Friday after a certain date (inclusive).
     *
     * @param date  the specified date
     * @return  the first Friday
     */
    private static LocalDate getFirstFriday(LocalDate date) {
        List<LocalDate> candidates = new ArrayList<>(7);
        for (int i = 0; i < 7; ++i) {
            candidates.add(date.plusDays(i));
        }
        return candidates.stream().filter(d -> d.getDayOfWeek() == DayOfWeek.FRIDAY).findFirst().get();
    }

    /**
     * Calculate the Easter Sunday of the specified year.
     * The algorithm used is the Lilius and Clavius algorithm. The naming of the variables follows
     * http://www.mathcs.duq.edu/simon/Fall11/easter.html and the comments are mainly from
     * http://www.henk-reints.nl/easter/index.htm?frame=easteralg1.htm.
     *
     * @param year  the year
     * @return  the Easter Sunday of the specified year
     */
    private static LocalDate calculateEaster(int year) {
        // g: the golden number, a sequence number in the 19-year Metonic cycle
        int g = (year % 19) + 1;
        // c: the number of the century
        int c = (year/100) + 1;
        // x: account for the difference between the Gregorian and the Julian calendar (3 leap years less every 4 centuries)
        int x = (3*c)/4 - 12;
        // z: correction for the inaccuracy of the Metonic cycle, which is about 8 days in 25 (Gregorian) centuries.
        int z = (8*c + 5)/25 - 5;
        /*
         * d: account for the weekday of 21 March.
         * Since a year is 1 day longer than 52 weeks, it comes 1 weekday later every year plus a leap day every 4 years,
         * resulting in 5 days every 4 years. x is used to correct d for the Gregorian non-leap centuries
         */
        int d = (5*year)/4 - x - 10;
        /*
         * e: epacta, the age of the moon at the start of the year.
         * Since a tropical year is 11 days longer than 12 synodic moons (a so called moon year), the full moon
         * comes 11 days sooner each year. In the same time the corrections by the z and x components are applied.
         * The second (correcting) step in the calculation of e results from the fact that a synodic moon is not
         * exactly 30 days but a bit less (29.53059), and the 11 days difference between the just mentioned moon year
         * and the tropical year is also not exact (10.89).
         */
        int e = (11*g + 20 + z - x) % 30;
        if (e == 24 || (e == 25 && g > 11))
            e += 1;
        /*
         * n: either the first full moon since the vernal equinox on March 21st or the last one before.
         * If it is before then a synodic month of 30 days is added.
         */
        int n = 44 - e;
        if (n < 21)
            n += 30;
        // n: first Sunday after the full moon
        n = n + 7 - ((d+n) % 7);
        int month = 3;
        if (n > 31) {
            // change the date to April
            n -= 31;
            month = 4;
        }
        return LocalDate.of(year, month, n);
    }

    /**
     * Get the set of fixed holidays in a year.
     * @param year  the year
     * @return  the set of fixed holidays
     */
    private static Set<LocalDate> getFixedHolidays(int year) {
        Set<LocalDate> fixedHolidays = new HashSet<>();
        // New Year's Eve and New Year's Day
        fixedHolidays.add(LocalDate.of(year, 1, 1));
        fixedHolidays.add(LocalDate.of(year, 12, 31));
        // Epiphany
        fixedHolidays.add(LocalDate.of(year, 1, 6));
        // Walpurgis Night and May 1st
        fixedHolidays.add(LocalDate.of(year, 4, 30));
        fixedHolidays.add(LocalDate.of(year, 5, 1));
        // National Day
        fixedHolidays.add(LocalDate.of(year, 6, 6));
        // Christmas Eve, Christmas Day and Boxing Day
        fixedHolidays.add(LocalDate.of(year, 12, 24));
        fixedHolidays.add(LocalDate.of(year, 12, 25));
        fixedHolidays.add(LocalDate.of(year, 12, 26));
        return fixedHolidays;
    }
}
