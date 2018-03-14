package com.evolvetechnology;

import com.evolvetechnology.vehicle.Car;
import com.evolvetechnology.vehicle.Motorbike;
import com.evolvetechnology.vehicle.Vehicle;
import org.junit.Test;

import java.time.*;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.*;

public class TollCalculatorTest {

  final static LocalDate MONDAY = LocalDate.of(2018, Month.MARCH, 12);
  final static LocalDate TUESDAY = LocalDate.of(2018, Month.MARCH, 13);
  final static LocalDate WEDNESDAY = LocalDate.of(2018, Month.MARCH, 14);
  final static LocalDate THURSDAY = LocalDate.of(2018, Month.MARCH, 15);
  final static LocalDate FRIDAY = LocalDate.of(2018, Month.MARCH, 16);
  final static LocalDate SATURDAY = LocalDate.of(2018, Month.MARCH, 17);
  final static LocalDate SUNDAY = LocalDate.of(2018, Month.MARCH, 18);

  @Test
  public void getTollFee() throws Exception {
    Vehicle car = new Car();
    Vehicle motorBike = new Motorbike();

    TollCalculator tollCalculator = new TollCalculator();

    final LocalDateTime monday18 = LocalDateTime.of(MONDAY, LocalTime.of(18, 00));
    final int actualCost = tollCalculator.getTollFee(car, monday18);

    final int expectedCost = getTollFeeSpec(car, convertLocalDate(monday18));
    assertEquals(expectedCost, actualCost);
  }

  public Date convertLocalDate(LocalDateTime localDateTime) {
    return Date.from(localDateTime.atZone(ZoneId.systemDefault()).toInstant());
  }

  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFeeSpec(Vehicle vehicle, Date... dates) {
    Date intervalStart = dates[0];
    int totalFee = 0;
    for (Date date : dates) {
      int nextFee = getTollFee(date, vehicle);
      int tempFee = getTollFee(intervalStart, vehicle);

      TimeUnit timeUnit = TimeUnit.MINUTES;
      long diffInMillies = date.getTime() - intervalStart.getTime();
      long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

      if (minutes <= 60) {
        if (totalFee > 0) totalFee -= tempFee;
        if (nextFee >= tempFee) tempFee = nextFee;
        totalFee += tempFee;
      } else {
        totalFee += nextFee;
      }
    }
    if (totalFee > 60) totalFee = 60;
    return totalFee;
  }

  private boolean isTollFreeVehicle(Vehicle vehicle) {
    if(vehicle == null) return false;
    String vehicleType = vehicle.getType();
    return vehicleType.equals(TollFreeVehicles.MOTORBIKE.getType()) ||
            vehicleType.equals(TollFreeVehicles.TRACTOR.getType()) ||
            vehicleType.equals(TollFreeVehicles.EMERGENCY.getType()) ||
            vehicleType.equals(TollFreeVehicles.DIPLOMAT.getType()) ||
            vehicleType.equals(TollFreeVehicles.FOREIGN.getType()) ||
            vehicleType.equals(TollFreeVehicles.MILITARY.getType());
  }

  public int getTollFee(final Date date, Vehicle vehicle) {
    if(isTollFreeDate(date) || isTollFreeVehicle(vehicle)) return 0;
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);
    int hour = calendar.get(Calendar.HOUR_OF_DAY);
    int minute = calendar.get(Calendar.MINUTE);

    if (hour == 6 && minute >= 0 && minute <= 29) return 8;
    else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
    else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
    else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
    else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
    else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
    else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
    else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
    else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
    else return 0;
  }

  private Boolean isTollFreeDate(Date date) {
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);
    int year = calendar.get(Calendar.YEAR);
    int month = calendar.get(Calendar.MONTH);
    int day = calendar.get(Calendar.DAY_OF_MONTH);

    int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
    if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

    if (year == 2013) {
      if (month == Calendar.JANUARY && day == 1 ||
              month == Calendar.MARCH && (day == 28 || day == 29) ||
              month == Calendar.APRIL && (day == 1 || day == 30) ||
              month == Calendar.MAY && (day == 1 || day == 8 || day == 9) ||
              month == Calendar.JUNE && (day == 5 || day == 6 || day == 21) ||
              month == Calendar.JULY ||
              month == Calendar.NOVEMBER && day == 1 ||
              month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31)) {
        return true;
      }
    }
    return false;
  }

  private enum TollFreeVehicles {
    MOTORBIKE("Motorbike"),
    TRACTOR("Tractor"),
    EMERGENCY("Emergency"),
    DIPLOMAT("Diplomat"),
    FOREIGN("Foreign"),
    MILITARY("Military");
    private final String type;

    TollFreeVehicles(String type) {
      this.type = type;
    }

    public String getType() {
      return type;
    }
  }

}