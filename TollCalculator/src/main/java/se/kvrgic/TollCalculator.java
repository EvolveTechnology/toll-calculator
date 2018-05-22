package se.kvrgic;

import java.text.SimpleDateFormat;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.concurrent.*;

public class TollCalculator {

    private static final int MAXFEE = 60;
    private static final SimpleDateFormat TIMEFORMAT = new SimpleDateFormat("HH:mm");
    private List<Vehicle> tolledVehicles = Arrays.asList(Vehicle.CAR);
    
  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
    public int getTollFee(Vehicle vehicle, Date... dates) {
        int totalFee = 0;
        Date intervalStart = dates[0];
        int intervalFee = 0;

        for (Date date : dates) {
            TimeUnit timeUnit = TimeUnit.MINUTES;
            long diffInMillies = date.getTime() - intervalStart.getTime();
            long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);
            if (minutes > 60) {
                totalFee += intervalFee;
                intervalStart = date;
                intervalFee = 0;
            }
            intervalFee = Math.max(intervalFee, getTollFee(date, vehicle));
        }
        totalFee += intervalFee;
        
        return (totalFee > MAXFEE) ? MAXFEE : totalFee;
    }

    private boolean isTollFreeVehicle(Vehicle vehicle) {
        if(vehicle == null) return false;
        return !tolledVehicles.contains(vehicle);
    }
  
    public int getTollFee(final Date date, Vehicle vehicle) {
          if (isTollFreeDate(date) || isTollFreeVehicle(vehicle)) return 0;
          String time = TIMEFORMAT.format(date);
  
          if      (isBetween(time, "06:00", "06:29")) return  8;
          else if (isBetween(time, "06:30", "06:59")) return 13;
          else if (isBetween(time, "07:00", "07:59")) return 18;
          else if (isBetween(time, "08:00", "08:29")) return 13;
          else if (isBetween(time, "08:30", "14:59")) return  8;
          else if (isBetween(time, "15:00", "15:29")) return 13;
          else if (isBetween(time, "15:30", "16:59")) return 18;
          else if (isBetween(time, "17:00", "17:59")) return 13;
          else if (isBetween(time, "18:00", "18:29")) return  8;
          else return 0;
    }
    
    /**
     * @param time - formated with TIMEFORMAT
     * @param leftInclusive
     * @param righInclusive
     * @return
     */
    private static boolean isBetween(String time, String leftInclusive, String rightInclusive) {
        return time.compareTo(leftInclusive)  >= 0 && 
               time.compareTo(rightInclusive) <= 0;
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
  
}

