package com.evolvetechnology;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;

/**
 * This UnchargedTimeResolver implementation decides if a date is toll free,
 * depending on if the date is holiday, a certain weekday or a certain month
 *
 */
public class UnchargedHolidayWeekDayAndMonthResolver implements UnchargedTimeResolver {

  private Set<LocalDate> tollFreeDays;
  private Set<LocalDate> tollFreeMonths;
  private Set<DayOfWeek> tollFreeWeekDays;

  private static int ANY_DAY = 1;
  private static int ANY_YEAR = 1;

  private UnchargedHolidayWeekDayAndMonthResolver() {
    tollFreeDays = new HashSet<>();
    tollFreeMonths = new HashSet<>();
    tollFreeWeekDays = new HashSet<>();
  }

  public static UnchargedHolidayWeekDayAndMonthResolver create() {
    return new UnchargedHolidayWeekDayAndMonthResolver();
  }

  public UnchargedHolidayWeekDayAndMonthResolver withTollFreeHolidays(Collection<LocalDate> tollFreeDates) {
    Collection<LocalDate> zeroYearResetLocalDates = tollFreeDates.stream()
            .map(date -> LocalDate.of(ANY_YEAR, date.getMonth(), date.getDayOfMonth()))
            .collect(Collectors.toCollection(ArrayList::new));
    this.tollFreeDays.addAll(zeroYearResetLocalDates);
    return this;
  }

  public UnchargedHolidayWeekDayAndMonthResolver withTollFreeMonths(Collection<LocalDate> tollFreeMonths) {
    Collection<LocalDate> zeroDayResetLocalDates = tollFreeMonths.stream()
            .map(date -> LocalDate.of(date.getYear(), date.getMonth(), ANY_DAY))
            .collect(Collectors.toCollection(ArrayList::new));
    this.tollFreeMonths.addAll(zeroDayResetLocalDates);
    return this;
  }

  public UnchargedHolidayWeekDayAndMonthResolver withTollFreeWeekDays(Collection<DayOfWeek> tollFreeWeekDays) {
    this.tollFreeWeekDays.addAll(tollFreeWeekDays);
    return this;
  }

  public boolean isTollFreeDate(LocalDateTime date) {
    return isTollFreeWeekDay(date) || isTollFreeMonth(date) || isTollFreeHoliday(date);
  }

  private boolean isTollFreeHoliday(LocalDateTime date) {
    return tollFreeDays.contains(LocalDate.of(ANY_YEAR, date.getMonth(), date.getDayOfMonth()));
  }

  private boolean isTollFreeMonth(LocalDateTime date) {
    return tollFreeMonths.contains(LocalDate.of(date.getYear(), date.getMonth(), ANY_DAY));
  }

  private boolean isTollFreeWeekDay(LocalDateTime date) {
    return tollFreeWeekDays.contains(date.getDayOfWeek());
  }
}
