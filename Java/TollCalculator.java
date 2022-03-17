
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.temporal.ChronoField;
import java.time.temporal.TemporalField;
import java.util.*;
import java.util.concurrent.*;

public class TollCalculator {

  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFee(Vehicle vehicle, Date... dates) {
    // I believe in this method the times 08:40 and 09:20 should consider as times of 2 different hours (i.e. hour of 8 and hour of 9.
    // In the previous logic if the 08:40 was the first of the array then 09:20 was considered as same hour because it has considered the time gap
    // Also the previous implementation was not updating the intervalStart correctly.

    int[] feeOfTheHour = new int[24];
    Arrays.fill(feeOfTheHour, 0);

    // calculate max fee for each hour
    for (Date date : dates) {
      int fee = getTollFee(date, vehicle);
      int hour = Instant.ofEpochMilli(date.getTime()).atZone(ZoneId.systemDefault()).getHour();

      if(feeOfTheHour[hour] < fee){
        feeOfTheHour[hour] = fee;
      }
    }

    // get the sum of hourly fee
    int totalFee = Arrays.stream(feeOfTheHour).sum();

    // cap the value
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
    else if (hour == 8 && minute >= 30 && minute <= 59) return 8;
    else if (hour >= 9 && hour <= 14 && minute >= 0 && minute <= 59) return 8;
    else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
    else if (hour == 15 && minute >= 30 && minute <= 59) return 18;
    else if (hour == 16 && minute >= 0 && minute <= 59) return 18;
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

    //TODO - consider every year differently or remove the year validation
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

