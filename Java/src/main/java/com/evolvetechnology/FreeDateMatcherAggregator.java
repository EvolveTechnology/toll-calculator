package com.evolvetechnology;

import java.time.LocalDateTime;
import java.util.*;
import java.util.function.Predicate;

public class FreeDateMatcherAggregator implements Predicate<LocalDateTime>  {

  private final Collection<Predicate<LocalDateTime>> freeDateTimeMatchers = new LinkedList<>();

  @SafeVarargs
  public FreeDateMatcherAggregator(Predicate<LocalDateTime>... freeDateTimeMatchers) {
    this.freeDateTimeMatchers.addAll(Arrays.asList(freeDateTimeMatchers));
  }

  @Override
  public boolean test(LocalDateTime localDateTime) {
    return freeDateTimeMatchers.stream()
            .reduce(Predicate::or)
            .orElse(date -> false)
            .test(localDateTime);
  }

}
