
import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;
import java.util.concurrent.TimeUnit;
import java.util.stream.Collectors;

public class TollCalculator {

  private final int MAX_VALUE = 60;

  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFee(Vehicle vehicle, Date... dates) {
    int totalFee = 0;
    List<Date> datesList = Arrays.stream(dates).sorted(Date::compareTo).collect(Collectors.toList());

    for (int i = 0; i < datesList.size();) {
      int j = i + 1;
      int tempFee = getTollFee(datesList.get(i), vehicle);
      if (tempFee == 0) {i++;continue;}
      while (j < datesList.size() &&
              TimeUnit.MILLISECONDS.toMinutes(datesList.get(j).getTime() - datesList.get(i).getTime()) <= 60) {
        tempFee = Math.max(tempFee, getTollFee(datesList.get(j), vehicle));
        j++;
      }
      totalFee += tempFee;
      if (totalFee >= MAX_VALUE) return MAX_VALUE;
      i = j;
    }

    return totalFee;
  }

  private boolean isTollFreeVehicle(Vehicle vehicle) {
    if(vehicle == null) return false;
    return vehicle.isFree();
  }

  private int getTollFee(final Date date, Vehicle vehicle) {
    if(isTollFreeDate(date) || isTollFreeVehicle(vehicle)) return 0;
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);
    int hour = calendar.get(Calendar.HOUR_OF_DAY);
    int minute = calendar.get(Calendar.MINUTE);

    if (hour == 6 && minute <= 29) return 8;
    else if (hour == 6) return 13;
    else if (hour == 7) return 18;
    else if (hour == 8 && minute <= 29) return 13;
    else if (hour >= 8 && hour <= 14) return 8;
    else if (hour == 15 && minute <= 29) return 13;
    else if (hour == 15 || hour == 16) return 18;
    else if (hour == 17) return 13;
    else if (hour == 18) return 8;
    else return 0;
  }

  private Boolean isTollFreeDate(Date date) {
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);
    int month = calendar.get(Calendar.MONTH);
    int day = calendar.get(Calendar.DAY_OF_MONTH);

    int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
    if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

    return month == Calendar.JANUARY && day == 1 ||
              month == Calendar.MARCH && (day == 28 || day == 29) ||
              month == Calendar.APRIL && (day == 1 || day == 30) ||
              month == Calendar.MAY && (day == 1 || day == 8 || day == 9) ||
              month == Calendar.JUNE && (day == 5 || day == 6 || day == 21) ||
              // all july is free, i understand it so
              month == Calendar.JULY ||
              month == Calendar.NOVEMBER && day == 1 ||
              month == Calendar.DECEMBER && (day == 24 || day == 25 || day == 26 || day == 31);
  }
}

