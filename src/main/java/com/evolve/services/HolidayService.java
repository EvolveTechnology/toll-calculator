package com.evolve.services;

import java.time.LocalDate;

public interface HolidayService {
    boolean isHoliday(LocalDate date);
}
