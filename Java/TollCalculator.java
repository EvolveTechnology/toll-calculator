package com.toll.calculator;

import java.util.*;
import java.util.concurrent.*;

import static com.toll.calculator.Constants.*;

public class TollCalculator {

  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFee(Vehicle vehicle, Date... dates) {
    Date intervalStart = dates[0];
    int totalFee = 0;
    for (Date date : dates) {
      int nextFee = getTollFee(date, vehicle);
      int tempFee = getTollFee(intervalStart, vehicle);

      TimeUnit timeUnit = TimeUnit.MINUTES;
      long diffInMillies = date.getTime() - intervalStart.getTime();
      long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

      if (minutes <= MAXIMUM) {
        if (totalFee > 0) totalFee -= tempFee;
        if (nextFee >= tempFee) tempFee = nextFee;
        totalFee += tempFee;
      } else {
        totalFee += nextFee;
      }
    }
    if (totalFee > MAXIMUM) totalFee = MAXIMUM;
    return totalFee;
  }

  private boolean isTollFreeVehicle(Vehicle vehicle) {
    if(vehicle == null) 
    	return false;
    return TollFreeVehicles.contains(vehicle.getType());
  }

  public int getTollFee(final Date date, Vehicle vehicle) {
    if(isTollFreeDate(date) || isTollFreeVehicle(vehicle)) return 0;
    GregorianCalendar calendar = new GregorianCalendar();
    calendar.setTime(date);
    return calculateToll(calendar);
  }

  private static int calculateToll(final Calendar calendar)
  {
    int hour = calendar.get(Calendar.HOUR_OF_DAY);
    int minute = calendar.get(Calendar.MINUTE);
    if ((hour == 6 || hour == 18) && minute <= 29) return MINIMUM;
    else if (hour == 6 || hour == 17 ) return 13;
    else if (hour == 8 && minute <= 29) return 13;
    else if (hour >= 8 && hour <= 14 && minute >= 30) return MINIMUM;
    else if (hour == 15 && minute <= 29) return 13;
    else if (hour == 7 || hour == 15 || hour == 16) return 18;
    return 0;
  }

  private boolean isTollFreeDate(Date date) {
    GregorianCalendar calendar = new GregorianCalendar();
    calendar.setTime(date);
    if(isWeekend(calendar)) return true;
    return isHoliday(calendar);
  }
  
  private static boolean isWeekend(final Calendar cal)
  {
      int day = cal.get(Calendar.DAY_OF_WEEK);
      return day == Calendar.SATURDAY || day == Calendar.SUNDAY;
  }

  private static boolean isHoliday(final Calendar calendar)
  {
    String month = calendar.getDisplayName(Calendar.MONTH, Calendar.LONG, Locale.getDefault());
    if (month.equalsIgnoreCase(JULY)) return true;
    String holidays = HolidayList.getDays(month);
    if (null == holidays) return false;
    List<String> holidayList = new ArrayList<>(Arrays.asList(holidays.split(",")));
    int day = calendar.get(Calendar.DAY_OF_MONTH);
    return holidayList.contains(Integer.toString(day));
  }
  
}

