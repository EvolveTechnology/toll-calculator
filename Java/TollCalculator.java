package tollfee;

import java.util.*;
import java.util.concurrent.*;

public class TollCalculator {

  /**
   * Get toll fee for a list of date-times for when a checkpoint is passed.
   * @param vehicle Vehicle type.
   * @param dates   List of dates (or a list of date + time of day to be exact).
   * @return        Fee as an integer.
   */
  public int getTollFee(Vehicle vehicle, ArrayList<Date> dates) {
    if (vehicle == null || dates == null) {
      throw new NullPointerException("Arguments have to be instanciated.");
    }

    // Group dates by day. This helps us check that we only pay at most 60 per day.
    // Note that days are seperated at 12.00am, which means that you will have to pay two fees if you pass
    // a checkpoint, for example,  at 23.59 and then again at 00.01.
    Map<Long, ArrayList<Date>> datesByDay = groupPerTimePeriod(dates, TimeUnit.DAYS);

    // Sum up daily toll fees for the period.
    int totalFee = 0;
    for (Map.Entry<Long, ArrayList<Date>> entry : datesByDay.entrySet()) {
      totalFee += getTollFeePerDay(vehicle, entry.getValue());
    }

    return totalFee;
  }

  /**
   * Calculate the total toll fee for one day.
   *
   * @param vehicle - the vehicle
   * @param dates - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  private int getTollFeePerDay(Vehicle vehicle, ArrayList<Date> dates) {
    int totalDayFee = 0;
    Collections.sort(dates);

    Date intervalStart = dates.iterator().next();
    int totalHourFee = 0;

    // Calculate fee per hour.
    for (Date date : dates) {
      int nextFee = getMomentaryTollFee(date, vehicle);
      long diffInMillies = date.getTime() - intervalStart.getTime();
      long minutes = TimeUnit.MILLISECONDS.toMinutes(diffInMillies);

      // Make sure we just pay once per hour. The fee should be the highest.
      // Since dates are sorted it is fine to only compare positive minutes.
      if (minutes <= 60) {
        if (nextFee > totalDayFee) {
          totalHourFee = nextFee;
        }
      } else {
        // Add previous hour fee to totalDayFee.
        totalDayFee += totalHourFee;
        // Start a new interval.
        intervalStart = date;
        totalHourFee = nextFee;
      }
    }
    totalDayFee += totalHourFee;

    // Add total dayly fee, but max 60 per day.
    return totalDayFee > 60 ? 60 : totalDayFee;
  }

  /**
   * Group dates in TimeUnit time periods (e.g. TimeUnit.DAYS or TimeUnit.HOURS).
   *
   * @param dates List of date objects
   * @param timePeriod Time period as a TimeUnit enum
   * @return A map grouped by time period in milliseconds
   */
  private Map<Long, ArrayList<Date>> groupPerTimePeriod(ArrayList<Date> dates, TimeUnit timePeriod) {
    long millisPerPeriod = timePeriod.toMillis(1);
    Map<Long, ArrayList<Date>> datesByTimePeriod = new HashMap<>();

    dates.forEach((date) -> {
      long period = date.getTime() / millisPerPeriod;
      ArrayList<Date> dayDates = datesByTimePeriod.get(period);
      if (dayDates == null) {
        dayDates = new ArrayList<>();
        datesByTimePeriod.put(period, dayDates);
      }
      dayDates.add(date);
    });

    return datesByTimePeriod;
  }

  private boolean isTollFreeVehicle(Vehicle vehicle) {
    if (vehicle == null) {
      return false;
    }
    String vehicleType = vehicle.getType();
    return vehicleType.equals(TollFreeVehicles.MOTORBIKE.getType())
        || vehicleType.equals(TollFreeVehicles.TRACTOR.getType())
        || vehicleType.equals(TollFreeVehicles.EMERGENCY.getType())
        || vehicleType.equals(TollFreeVehicles.DIPLOMAT.getType())
        || vehicleType.equals(TollFreeVehicles.FOREIGN.getType())
        || vehicleType.equals(TollFreeVehicles.MILITARY.getType());
  }

  /**
   * Get toll fee amount for a specific time and vehicle.
   *
   * @param time - Time of day as a Date object
   * @param vehicle - Vehicle type
   * @return
   */
  private int getMomentaryTollFee(final Date time, Vehicle vehicle) {
    if (isTollFreeDate(time) || isTollFreeVehicle(vehicle)) {
      return 0;
    }
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(time);
    int hour = calendar.get(Calendar.HOUR_OF_DAY);
    int minute = calendar.get(Calendar.MINUTE);

    if (hour == 6 && minute <= 29) {
      return 8;
    } else if (hour == 6 && minute >= 30) {
      return 13;
    } else if (hour == 7) {
      return 18;
    } else if (hour == 8 && minute <= 29) {
      return 13;
    } else if (hour >= 8 && hour <= 14) {
      return 8;
    } else if (hour == 15 && minute <= 29) {
      return 13;
    } else if (hour == 15 || hour == 16) {
      return 18;
    } else if (hour == 17) {
      return 13;
    } else if (hour == 18 && minute <= 29) {
      return 8;
    } else {
      return 0;
    }
  }

  private Boolean isTollFreeDate(Date date) {
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);
    int year = calendar.get(Calendar.YEAR);
    int month = calendar.get(Calendar.MONTH);
    int day = calendar.get(Calendar.DAY_OF_MONTH);

    int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
    if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) {
      return true;
    }

    if (year == 2013) {
      if (month == Calendar.JANUARY && day == 1
          || month == Calendar.MARCH && (day == 28 || day == 29)
          || month == Calendar.APRIL && (day == 1 || day == 30)
          || month == Calendar.MAY && (day == 1 || day == 8 || day == 9)
          || month == Calendar.JUNE && (day == 5 || day == 6 || day == 21)
          || month == Calendar.JULY
          || month == Calendar.NOVEMBER && day == 1
          || month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31)) {
        return true;
      }
    }
    return false;
  }

  private enum TimePeriod {
    DAY,
    HOUR;
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
