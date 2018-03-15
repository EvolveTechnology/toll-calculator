package com.evolvetechnology;

import com.evolvetechnology.vehicle.Car;
import com.evolvetechnology.vehicle.Motorbike;
import com.evolvetechnology.vehicle.Vehicle;
import org.junit.Test;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.Iterator;
import java.util.TreeSet;
import java.util.Set;

import static org.junit.Assert.*;

public class TollCalculatorTest {

  final static LocalDate MONDAY = LocalDate.of(2018, Month.MARCH, 12);
  final static LocalDate TUESDAY = LocalDate.of(2018, Month.MARCH, 13);
  final static LocalDate WEDNESDAY = LocalDate.of(2018, Month.MARCH, 14);
  final static LocalDate THURSDAY = LocalDate.of(2018, Month.MARCH, 15);
  final static LocalDate FRIDAY = LocalDate.of(2018, Month.MARCH, 16);
  final static LocalDate SATURDAY = LocalDate.of(2018, Month.MARCH, 17);
  final static LocalDate SUNDAY = LocalDate.of(2018, Month.MARCH, 18);

  // TODO: test for each spec in github
  // TODO: date manager with list of week days with cost, and not, and tollfree days(contains time cost manager)
  @Test
  public void getTollFee() throws Exception {
    Vehicle car = new Car();

    TollCalculator tollCalculator = new TollCalculator();

    final LocalDateTime monday18 = LocalDateTime.of(MONDAY, LocalTime.of(18, 0));
    final LocalDateTime tuesday18 = LocalDateTime.of(TUESDAY, LocalTime.of(18, 0));
    final int actualCost = tollCalculator.getTollFee(car, monday18, tuesday18);
    final int expectedCost = getTollFeeSpec(car, monday18, tuesday18);

    assertEquals(expectedCost, actualCost);
  }


  public int getTollFeeSpec(Vehicle vehicle, LocalDateTime... dates) {
    LocalDateTime intervalStart = dates[0];

    int totalFee = 0;
    for (LocalDateTime date : dates) {
      int nextFee = getTollFee(date, vehicle);
      int tempFee = getTollFee(intervalStart, vehicle);

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

  public int getTollFee(final LocalDateTime date, Vehicle vehicle) {
    if (isTollFreeDate(date) || vehicle.isTollFree()) return 0;

    CostIntervals costIntervals = new CostIntervals();
    costIntervals.add(LocalTime.of(6, 0), LocalTime.of(6, 30), 8);
    costIntervals.add(LocalTime.of(6, 30), LocalTime.of(7, 0), 13);
    costIntervals.add(LocalTime.of(7, 0), LocalTime.of(8, 0), 18);
    costIntervals.add(LocalTime.of(8, 0), LocalTime.of(8, 30), 13);
    costIntervals.add(LocalTime.of(8, 30), LocalTime.of(15, 0), 8);
    costIntervals.add(LocalTime.of(15, 0), LocalTime.of(15, 30), 13);
    costIntervals.add(LocalTime.of(15, 30), LocalTime.of(17, 0), 18);
    costIntervals.add(LocalTime.of(17, 0), LocalTime.of(18, 0), 13);
    costIntervals.add(LocalTime.of(18, 0), LocalTime.of(18, 30), 8);
    return costIntervals.getCostFor(date.toLocalTime());
  }

  private Boolean isTollFreeDate(LocalDateTime date) {
    if (isWeekend(date)) return true;
    if (date.getYear() == 2013 && date.getMonth() == Month.JULY) return true;

    Set<LocalDate> tollFreeDays = new TreeSet<>();
    tollFreeDays.add(LocalDate.of(2013, Month.JANUARY, 1));
    tollFreeDays.add(LocalDate.of(2013, Month.APRIL, 1));
    tollFreeDays.add(LocalDate.of(2013, Month.APRIL, 30));
    tollFreeDays.add(LocalDate.of(2013, Month.MAY, 1));
    tollFreeDays.add(LocalDate.of(2013, Month.MAY, 8));
    tollFreeDays.add(LocalDate.of(2013, Month.MAY, 9));
    tollFreeDays.add(LocalDate.of(2013, Month.JUNE, 5));
    tollFreeDays.add(LocalDate.of(2013, Month.JUNE, 6));
    tollFreeDays.add(LocalDate.of(2013, Month.JUNE, 21));
    tollFreeDays.add(LocalDate.of(2013, Month.NOVEMBER, 1));
    tollFreeDays.add(LocalDate.of(2013, Month.DECEMBER, 24));
    tollFreeDays.add(LocalDate.of(2013, Month.DECEMBER, 25));
    tollFreeDays.add(LocalDate.of(2013, Month.DECEMBER, 26));
    tollFreeDays.add(LocalDate.of(2013, Month.DECEMBER, 31));

    return tollFreeDays.contains(date.toLocalDate());

  }

  private boolean isWeekend(LocalDateTime date) {
    return date.getDayOfWeek() == DayOfWeek.SATURDAY || date.getDayOfWeek() == DayOfWeek.SUNDAY;
  }

}