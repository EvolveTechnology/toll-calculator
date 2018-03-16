package com.evolvetechnology;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import java.util.function.Predicate;
import java.util.stream.Collectors;

public class FreeHolidayMatcher implements Predicate<LocalDateTime> {

  private Set<LocalDate> holidays = new HashSet<>();
  private static int ANY_YEAR = 1;

  public FreeHolidayMatcher(Set<LocalDate> holidays) {
    Collection<LocalDate> zeroYearResetLocalDates = holidays.stream()
            .map(date -> LocalDate.of(ANY_YEAR, date.getMonth(), date.getDayOfMonth()))
            .collect(Collectors.toCollection(ArrayList::new));
    this.holidays.addAll(zeroYearResetLocalDates);
  }

  @Override
  public boolean test(LocalDateTime localDateTime) {
    return isTollFreeHoliday(localDateTime);
  }

  private boolean isTollFreeHoliday(LocalDateTime date) {
    return holidays.contains(LocalDate.of(ANY_YEAR, date.getMonth(), date.getDayOfMonth()));
  }

}
