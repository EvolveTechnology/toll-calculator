package com.evolvetechnology;

import com.evolvetechnology.vehicle.Vehicle;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.*;
import java.util.stream.Collectors;

public class TollCalculator {

  private TimeCostManager timeCostManager;
  private Set<LocalDate> tollFreeDays;
  private Set<LocalDate> tollFreeMonths;
  private Set<DayOfWeek> tollFreeWeekDays;

  private static int ANY_DAY = 1;
  private static int ANY_YEAR = 1;

  public TollCalculator() {
    timeCostManager = new TimeCostManager();
    tollFreeDays = new TreeSet<>();
    tollFreeMonths = new TreeSet<>();
    tollFreeWeekDays = new HashSet<>();
  }

  public int getTollFee(Vehicle vehicle, LocalDateTime... dates) {
    LocalDateTime intervalStart = dates[0];

    int totalFee = 0;
    for (LocalDateTime date : dates) {
      int nextFee = this.getTollFee(vehicle, date);
      int tempFee = this.getTollFee(vehicle, intervalStart);

      long minutes = intervalStart.until(date, ChronoUnit.MINUTES);

      if (minutes <= 60) {
        if (totalFee > 0) continue;
        if (nextFee >= tempFee) tempFee = nextFee;
        totalFee += tempFee;
      } else {
        totalFee += nextFee;
      }
    }
    if (totalFee > 60) totalFee = 60;
    return totalFee;
  }

  public int getTollFee(Vehicle vehicle, final LocalDateTime date) {
    if (isTollFreeDate(date) || vehicle.isTollFree()) return 0;
    return timeCostManager.getCostFor(date.toLocalTime());
  }

  public void addCostIntervals(Collection<CostInterval> costIntervals) {
    this.timeCostManager.addAll(costIntervals);
  }

  public void addTollFreeDates(Collection<LocalDate> tollFreeDates) {
    Collection<LocalDate> zeroYearResetLocalDates = tollFreeDates.stream()
            .map(date -> LocalDate.of(ANY_YEAR, date.getMonth(), date.getDayOfMonth()))
            .collect(Collectors.toCollection(ArrayList::new));
    this.tollFreeDays.addAll(zeroYearResetLocalDates);
  }

  public void addTollFreeMonths(Collection<LocalDate> tollFreeMonths) {
    Collection<LocalDate> zeroDayResetLocalDates = tollFreeMonths.stream()
            .map(date -> LocalDate.of(date.getYear(), date.getMonth(), ANY_DAY))
            .collect(Collectors.toCollection(ArrayList::new));
    this.tollFreeMonths.addAll(zeroDayResetLocalDates);
  }

  public void addTollFreeWeekDays(Collection<DayOfWeek> tollFreeWeekDays) {
    this.tollFreeWeekDays.addAll(tollFreeWeekDays);
  }

  private Boolean isTollFreeDate(LocalDateTime date) {
    return isTollFreeWeekDay(date) || isTollFreeMonth(date) || isTollFreeDay(date);
  }

  private boolean isTollFreeDay(LocalDateTime date) {
    return tollFreeDays.contains(LocalDate.of(ANY_YEAR, date.getMonth(), date.getDayOfMonth()));
  }

  private boolean isTollFreeMonth(LocalDateTime date) {
    return tollFreeMonths.contains(LocalDate.of(date.getYear(), date.getMonth(), ANY_DAY));
  }

  private boolean isTollFreeWeekDay(LocalDateTime date) {
    return tollFreeWeekDays.contains(date.getDayOfWeek());
  }
}

