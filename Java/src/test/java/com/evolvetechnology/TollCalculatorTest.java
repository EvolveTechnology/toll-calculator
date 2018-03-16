package com.evolvetechnology;

import com.evolvetechnology.vehicle.*;
import com.google.common.collect.ImmutableList;
import com.google.common.collect.ImmutableSet;
import org.junit.Before;
import org.junit.Test;

import java.time.*;
import java.util.Collection;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

import static org.hamcrest.CoreMatchers.everyItem;
import static org.hamcrest.Matchers.*;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertThat;

public class TollCalculatorTest {

  private final static LocalDate NON_FREE_REGULAR_DAY = LocalDate.of(2018, Month.MARCH, 12);
  private static final Vehicle NON_FREE_VEHICLE = new Car();
  private static final int RUSH_HOUR_COST = 18;
  private static final LocalDateTime MORNING_RUSH_HOUR = LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(7, 0));
  private static final LocalDateTime AFTERNOON_RUSH_HOUR = LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(16, 0));

  private static ImmutableList<LocalDateTime> ALL_HOURS = ImmutableList.of(
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(6, 0)),
          MORNING_RUSH_HOUR,
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(8, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(9, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(10, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(11, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(12, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(13, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(14, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(15, 0)),
          AFTERNOON_RUSH_HOUR,
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(17, 0)),
          LocalDateTime.of(NON_FREE_REGULAR_DAY, LocalTime.of(18, 0))
  );

  private static TimeCostCalculator ALWAYS_CHARGING_TIME_COST_CALCULATOR = date -> 1000;
  private TimeCostCalculator timeCostCalculator;

  private UnchargedPredicateAggregator unchargedPredicateAggregator;

  @Before
  public void setUp() throws Exception {
    timeCostCalculator = IntervalTimeCostCalculator.create()
            .withCostIntervals(createCostIntervals());

    unchargedPredicateAggregator = new UnchargedPredicateAggregator(
            new UnchargedHolidayPredicate(createTollFreeDays()),
            new UnchargedMonthPredicate(createTollFreeMonths()),
            new UnchargedWeekdayPredicate(createTollFreeWeekDays())
    );
  }

  private Collection<CostInterval> createCostIntervals() {
    return ImmutableList.of(
            new CostInterval(LocalTime.of(6, 0), LocalTime.of(6, 30), 8),
            new CostInterval(LocalTime.of(6, 30), LocalTime.of(7, 0), 13),
            new CostInterval(LocalTime.of(7, 0), LocalTime.of(8, 0), RUSH_HOUR_COST),
            new CostInterval(LocalTime.of(8, 0), LocalTime.of(8, 30), 13),
            new CostInterval(LocalTime.of(8, 30), LocalTime.of(15, 0), 8),
            new CostInterval(LocalTime.of(15, 0), LocalTime.of(15, 30), 13),
            new CostInterval(LocalTime.of(15, 30), LocalTime.of(17, 0), RUSH_HOUR_COST),
            new CostInterval(LocalTime.of(17, 0), LocalTime.of(18, 0), 13),
            new CostInterval(LocalTime.of(18, 0), LocalTime.of(18, 30), 8));
  }

  private Set<LocalDate> createTollFreeDays() {
    return ImmutableSet.of(
            LocalDate.of(0, Month.JANUARY, 1),
            LocalDate.of(0, Month.APRIL, 1),
            LocalDate.of(0, Month.APRIL, 30),
            LocalDate.of(0, Month.MAY, 1),
            LocalDate.of(0, Month.MAY, 8),
            LocalDate.of(0, Month.MAY, 9),
            LocalDate.of(0, Month.JUNE, 5),
            LocalDate.of(0, Month.JUNE, 6),
            LocalDate.of(0, Month.JUNE, 21),
            LocalDate.of(0, Month.NOVEMBER, 1),
            LocalDate.of(0, Month.DECEMBER, 24),
            LocalDate.of(0, Month.DECEMBER, 25),
            LocalDate.of(0, Month.DECEMBER, 26),
            LocalDate.of(0, Month.DECEMBER, 31));
  }

  private Set<LocalDate> createTollFreeMonths() {
    return ImmutableSet.of(LocalDate.of(2013, Month.JULY, 1));
  }

  private Set<DayOfWeek> createTollFreeWeekDays() {
    return ImmutableSet.of(DayOfWeek.SATURDAY, DayOfWeek.SUNDAY);
  }

  @Test
  public void feesDifferBetween8And18() {
    TollCalculator tollCalculator = new TollCalculator(date -> false, timeCostCalculator);

    List<Integer> costsForDay = ALL_HOURS.stream()
            .map(hour -> tollCalculator.getTollFee(NON_FREE_VEHICLE, hour))
            .collect(Collectors.toList());
    assertThat(costsForDay, everyItem(is(both(greaterThanOrEqualTo(8)).and(lessThanOrEqualTo(18)))));
  }

  @Test
  public void rushHourYieldHighestFee() {
    TollCalculator tollCalculator = new TollCalculator(date -> false, timeCostCalculator);

    for (LocalDateTime hour : ALL_HOURS) {
      if (!isRushHour(hour)) {
        assertThat(tollCalculator.getTollFee(NON_FREE_VEHICLE, hour), is(lessThan(RUSH_HOUR_COST)));
      }
    }
  }

  private boolean isRushHour(LocalDateTime hour) {
    return hour.equals(MORNING_RUSH_HOUR) || hour.equals(AFTERNOON_RUSH_HOUR);
  }

  @Test
  public void maximumFeeIs60() {
    TollCalculator tollCalculator = new TollCalculator(date -> false, timeCostCalculator);

    LocalDateTime[] allHours = ALL_HOURS.toArray(new LocalDateTime[ALL_HOURS.size()]);
    int costForAllHours = tollCalculator.getTollFee(NON_FREE_VEHICLE, allHours);
    assertEquals(60, costForAllHours);
  }

  @Test
  public void vehicleShouldOnlyBeChargedOnceAnHour() {
    TollCalculator tollCalculator = new TollCalculator(date -> false, timeCostCalculator);

    int cost = tollCalculator.getTollFee(NON_FREE_VEHICLE, MORNING_RUSH_HOUR, MORNING_RUSH_HOUR.plusMinutes(20));
    assertEquals(RUSH_HOUR_COST, cost);
  }

  @Test
  public void someVehiclesAreTollFree() {
    TollCalculator tollCalculator = new TollCalculator(date -> false, ALWAYS_CHARGING_TIME_COST_CALCULATOR);

    LocalDateTime[] allHours = ALL_HOURS.toArray(new LocalDateTime[ALL_HOURS.size()]);
    int allFreeVehiclesCost = tollCalculator.getTollFee(new Military(), allHours) +
            tollCalculator.getTollFee(new Tractor(), allHours) +
            tollCalculator.getTollFee(new Foreign(), allHours) +
            tollCalculator.getTollFee(new Emergency(), allHours) +
            tollCalculator.getTollFee(new Diplomat(), allHours);
    assertEquals(0, allFreeVehiclesCost);
  }

  @Test
  public void weekendsAndHolidaysAreTollFree() {
    TollCalculator tollCalculator = new TollCalculator(unchargedPredicateAggregator, ALWAYS_CHARGING_TIME_COST_CALCULATOR);

    LocalDateTime newYearsEve = LocalDateTime.of(2018, 12, 31, 0, 0);
    LocalDateTime saturday = LocalDateTime.of(2018, Month.MARCH, 17, 0, 0);
    LocalDateTime sunday = LocalDateTime.of(2018, Month.MARCH, 18, 0, 0);
    int costForWeekendAndHoliday = tollCalculator.getTollFee(NON_FREE_VEHICLE, newYearsEve, saturday, sunday);
    assertEquals(0, costForWeekendAndHoliday);
  }

}