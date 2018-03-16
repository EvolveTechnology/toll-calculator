package com.evolvetechnology;

import java.time.LocalDateTime;
import java.util.*;
import java.util.function.Predicate;

public class UnchargedPredicateAggregator implements Predicate<LocalDateTime>  {

  private final Collection<Predicate<LocalDateTime>> unchargedTimeResolvers = new LinkedList<>();

  @SafeVarargs
  public UnchargedPredicateAggregator(Predicate<LocalDateTime>... unchargedTimeResolvers) {
    this.unchargedTimeResolvers.addAll(Arrays.asList(unchargedTimeResolvers));
  }

  @Override
  public boolean test(LocalDateTime localDateTime) {
    return unchargedTimeResolvers.stream()
            .reduce(Predicate::or)
            .orElse(date -> false)
            .test(localDateTime);
  }
}
