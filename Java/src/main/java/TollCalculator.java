import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import vehicles.Vehicle;

import java.util.*;
import java.util.concurrent.TimeUnit;

class InvalidDataException extends Exception {
  InvalidDataException(String message) {
    super(message);
  }
}

class TollCalculator {
  private static final Logger logger = LogManager.getLogger(TollCalculator.class);
  static final int MAX_TOLL = 60;

  /**
   * Calculate the total toll fee for one day considering the type of vehicle
   *
   * @param vehicle - the vehicle
   * @param dates - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFee(final Vehicle vehicle, Date... dates) throws InvalidDataException {
    logger.info("Calculating toll for vehicle {} on dates {}!", vehicle, dates);
    validateInputs(vehicle, dates);

    if (isTollFreeVehicle(vehicle)) {
      logger.debug("vehicle {} is toll free", vehicle);
      return 0;
    }

    Date intervalStart = dates[0];
    if (isTollFreeDate(intervalStart)) {
      logger.info("Date {} is toll free", intervalStart.toString());
      return 0;
    }

    int totalFee = 0;
    for (Date date : dates) {
      int nextFee = calculateTollFee(date);
      int tempFee = calculateTollFee(intervalStart);

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
    if (totalFee > MAX_TOLL) {
      totalFee = MAX_TOLL;
    }
    return totalFee;
  }

  /** Calculate the toll fee for the given time of the day. This function does not consider the date itself.
   * @param date the date
   * @return toll fee for the given time of the day
   */
  public int calculateTollFee(final Date date) {
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);
    int hour = calendar.get(Calendar.HOUR_OF_DAY);
    int minute = calendar.get(Calendar.MINUTE);

    if (hour == 6) {
      return (minute <= 29) ? 8 : 13;
    } else if (hour == 7) {
      return 18;
    } else if (hour == 8) {
      return (minute <= 29) ? 13 : 8;
    } else if (hour >= 9 && hour <= 14) {
      return (minute <= 29) ? 0 : 8;
    } else if (hour == 15) {
      return (minute <= 29) ? 13 : 18;
    } else if (hour == 16) {
      return 18;
    } else if (hour == 17) {
      return 13;
    } else if (hour == 18) {
      return (minute <= 29) ? 8 : 0;
    }
    return 0;
  }


  private void validateInputs(Vehicle vehicle, Date[] dates) throws InvalidDataException {
    if (vehicle == null) throw new InvalidDataException("Vehicle can not be null");

    if (Arrays.stream(dates).anyMatch(Objects::isNull)) {
      logger.error("Date list contains NULL.");
      throw new InvalidDataException("Date list contains NULL.");
    }
//    TODO maybe need to verify that all given dates are for ONE day
  }


  private boolean isTollFreeVehicle(Vehicle vehicle) {
    String vehicleType = vehicle.getType();
    for (TollFreeVehicles value : TollFreeVehicles.values()) {
      if (vehicleType.equalsIgnoreCase(value.getType())) {
        return true;
      }
    }
    return false;
  }

  private Boolean isTollFreeDate(Date date) {
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);

    int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
    if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) {
      logger.info("{} is a weekend", date);
      return true;
    }

    int year = calendar.get(Calendar.YEAR);
    int month = calendar.get(Calendar.MONTH);
    int day = calendar.get(Calendar.DAY_OF_MONTH);
    return ApiHelper.isHoliday(year, month, day);
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
