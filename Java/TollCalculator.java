import java.util.*;
import java.util.concurrent.*;
import java.util.stream.Collectors;

public class TollCalculator {

  // Update: Store the current vehicle global..
  private Vehicle vehicle;
  // Update: Keep in track of the status of all dates
  private ArrayList<VehicleTime> vehicleTimes = new ArrayList<>();

  // Update: Add a custructor so we dont need to pass the same vehicle to all the functions
  // Aslo all current dates 
  public TollCalculator(Vehicle vehicle, ArrayList<Date> dates){
    this.vehicle = vehicle;
    for(Date date : dates){
      VehicleTime VehicleTime = new VehicleTime(date);
      vehicleTimes.add(VehicleTime);
    }
    // Calulate all the current fees directly..
    getTotalTollFee();
  }

  // Update: An inner class that keep in track of all dates have been calulated or not
  // Good to save memory
  private static class VehicleTime {
    private Date date;
    private boolean isCheckedForFee = false;
    private Integer fee;
    public VehicleTime(Date date){
      this.date = date;
    }
    public Date getDate(){
      return this.date;
    }
    public void setIsCheckedForFee(){
      this.isCheckedForFee = true;
    }
    public boolean isCheckedForFee(){
      return this.isCheckedForFee;
    }
    public void setFee(int fee){
      this.fee = fee;
    }
    public int getFee(){
      if(Helpers.isNull(this.fee)) return 0;
      return this.fee;
    }
  }

  /**
   * Calculate the total toll fee for one day
   *
   * @return - the total toll fee for that day
   */
  // Update: Changed to no inputs needed. Just using the global time-variable
  public int getTotalTollFee() {

    // If nothing exist, break the code..
    if(vehicleTimes.size() == 0) return 0;

    // Get the unchecked dates
    ArrayList<VehicleTime> unCheckedVehicleTimes = vehicleTimes
                .stream()
                .filter(p ->
                    !p.isCheckedForFee()
                )
                .collect(Collectors.toCollection(ArrayList::new));
    // Return the total if already counted
    if(unCheckedVehicleTimes.size() == 0) return getTotalFee();

    Date intervalStart = unCheckedVehicleTimes.get(0).getDate();
    int totalFee = 0;
    for (VehicleTime vehicleTime : unCheckedVehicleTimes) {

      int nextFee = getTollFeeDuringClock(vehicleTime.getDate());
      int tempFee = getTollFeeDuringClock(intervalStart);

      TimeUnit timeUnit = TimeUnit.MINUTES;
      long diffInMillies = vehicleTime.getDate().getTime() - intervalStart.getTime();
      long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

      if (minutes <= 60) {
        if (totalFee > 0) totalFee -= tempFee;
        if (nextFee >= tempFee) tempFee = nextFee;
        totalFee += tempFee;
      } else {
        totalFee += nextFee;
      }

      // Update: Change the state and save the fee
      vehicleTime.setIsCheckedForFee();
      vehicleTime.setFee(totalFee);
    }

    // Update: Check from the stored list
    return getTotalFee();
  }

  // Update: Get the current total fee
  private int getTotalFee(){
    int currentTotalFee = 0;
    for(VehicleTime vehicleTime : vehicleTimes){
      currentTotalFee += vehicleTime.getFee();
    }
    if (currentTotalFee > 60) currentTotalFee = 60;
    return currentTotalFee;
  }

  // Update: I think its a better name 'getTollFeeDuringClock' than 'getTollFee', cause i was not sure first what this function did..
  public int getTollFeeDuringClock(final Date date) {

    // Update: Input validation..
    if(Helpers.isNull(date)) return 0;

    // Update: Changing the date to GregorianCalendar here so we dont need to do it again
    MyCalendar calendar = new MyCalendar(date);

    if(isTollFreeDate(calendar) || TollFreeVehicles.isTollFreeVehicle(vehicle)) return 0;
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

  // Update: Can use a primitive returntype, cause the function does not return null.
  // Update: Input as GregorianCalendar, cause we already have that data now
  private boolean isTollFreeDate(MyCalendar calendar) {

    // Update: Input validation..
    if(Helpers.isNull(calendar)) return false;
    
    int year = calendar.get(Calendar.YEAR);
    int month = calendar.get(Calendar.MONTH);
    int day = calendar.get(Calendar.DAY_OF_MONTH);

    int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
    if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

    // (TODO) Update: Just break the code here, until someone adds the current year
    MyCalendar myCalendar = new MyCalendar(new Date());
    int currentYear = myCalendar.get(Calendar.YEAR);
    if(currentYear != year){
        return false;
    }
    
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

    // Update: Changing to private. Public not needed anymore...
    private String getType() {
      return type;
    }

    /** Update: Added simple method to check if the current vehicle is a tollfree one..
    No need for If-checks, just using ArrayLists stream-feature
    Also saves time when adding/removing items from this enum,
    so we dont need to add logic to the rest of the application. */
    public static boolean isTollFreeVehicle(Vehicle vehicle){
      if(Helpers.isNull(vehicle)) return false;
      String vehicleType = vehicle.getType();
      if(Helpers.stringIsNullOrEmpty(vehicleType)) return false;
      return !Helpers.isNull(new ArrayList<>(Arrays.asList(TollFreeVehicles.values()))
        .stream()
        .filter(p -> p.getType().equalsIgnoreCase(vehicleType))
        .findFirst().orElse(null));
    }
  }
}