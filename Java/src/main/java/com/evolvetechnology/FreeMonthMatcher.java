package com.evolvetechnology;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.*;
import java.util.function.Predicate;
import java.util.stream.Collectors;

public class FreeMonthMatcher implements Predicate<LocalDateTime> {

  private static final int ANY_DAY = 1;
  private final Set<LocalDate> tollFreeMonths = new HashSet<>();

  public FreeMonthMatcher(Set<LocalDate> months) {
    Collection<LocalDate> zeroDayResetLocalDates = months.stream()
            .map(date -> LocalDate.of(date.getYear(), date.getMonth(), ANY_DAY))
            .collect(Collectors.toCollection(ArrayList::new));
    this.tollFreeMonths.addAll(zeroDayResetLocalDates);
  }

  @Override
  public boolean test(LocalDateTime localDateTime) {
    return isTollFreeMonth(localDateTime);
  }

  private boolean isTollFreeMonth(LocalDateTime date) {
    return tollFreeMonths.contains(LocalDate.of(date.getYear(), date.getMonth(), ANY_DAY));
  }

}
