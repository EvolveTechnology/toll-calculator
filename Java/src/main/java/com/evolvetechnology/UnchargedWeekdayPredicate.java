package com.evolvetechnology;

import java.time.DayOfWeek;
import java.time.LocalDateTime;
import java.util.HashSet;
import java.util.Set;
import java.util.function.Predicate;

public class UnchargedWeekdayPredicate implements Predicate<LocalDateTime> {
  private Set<DayOfWeek> tollFreeWeekDays = new HashSet<>();

  public UnchargedWeekdayPredicate(Set<DayOfWeek> tollFreeWeekDays) {
    this.tollFreeWeekDays.addAll(tollFreeWeekDays);
  }

  @Override
  public boolean test(LocalDateTime localDateTime) {
    return isTollFreeWeekDay(localDateTime.getDayOfWeek());
  }

  private boolean isTollFreeWeekDay(DayOfWeek dayOfWeek) {
    return tollFreeWeekDays.contains(dayOfWeek);
  }


}
