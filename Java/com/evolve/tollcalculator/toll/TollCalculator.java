package com.evolve.tollcalculator.toll;

import com.evolve.tollcalculator.holiday.HolidayService;
import com.evolve.tollcalculator.holiday.StaticHolidayService;
import com.evolve.tollcalculator.vehicle.Vehicle;

import java.time.*;
import java.util.*;
import java.util.stream.Collectors;

import static java.util.Arrays.asList;

public class TollCalculator {
  private final static List<Month> TOLL_FREE_MONTHS = asList(
          Month.JULY
  );

  private final HolidayService holidayService;

  public TollCalculator() {
    //Normally use DI for this
    this.holidayService = new StaticHolidayService();
  }

  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param tollPasses   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFee(final Vehicle vehicle, final List<LocalDateTime> tollPasses) {
    if(vehicle.isTollFree()) {
      return 0;
    }

    List<TollPassGroup> tollPassGroups = new ArrayList<>();

    for(LocalDateTime currentTollPass : tollPasses.stream()
            .sorted(LocalDateTime::compareTo)
            .collect(Collectors.toList())) {
      if(tollPassGroups.stream()
                      .noneMatch(tollPassGroup -> tollPassGroup.tollPassExists(currentTollPass))) {

        TollPassGroup group = new TollPassGroup(currentTollPass);

        group.addTollPasses(
          tollPasses.stream()
                  .filter(tollPass -> group.isApplicable(tollPass))
                  .collect(Collectors.toList())
        );

        tollPassGroups.add(group);
      }
    }

    int totalFee = 0;

    for(TollPassGroup group : tollPassGroups) {
      totalFee += group.getTollPasses().stream()
              .map(date -> getTollFee(date, vehicle))
              .max(Integer::compareTo)
              .orElse(0);
    }

    return totalFee > 60 ? 60 : totalFee;
  }

  public int getTollFee(final LocalDateTime date, final Vehicle vehicle) {
    if(isTollFreeDate(date) || vehicle.isTollFree()) {
      return 0;
    }

    return TollFees.get(LocalTime.of(date.getHour(), date.getMinute()));
  }

  private boolean isTollFreeDate(final LocalDateTime date) {
    return isWeekend(date) || isTollFreeMonth(date) || holidayService.isHoliday(date.toLocalDate());
  }

  private boolean isWeekend(final LocalDateTime date) {
    DayOfWeek dayOfWeek = date.getDayOfWeek();

    return dayOfWeek.equals(DayOfWeek.SATURDAY) || dayOfWeek.equals(DayOfWeek.SUNDAY);
  }

  private boolean isTollFreeMonth(final LocalDateTime date) {
    return TOLL_FREE_MONTHS.stream().anyMatch(month -> month.equals(date.getMonth()));
  }
}

