package com.evolvetechnology;

import java.time.LocalDateTime;

@FunctionalInterface
public interface UnchargedTimeResolver {
  boolean isTollFreeDate(LocalDateTime date);
}
