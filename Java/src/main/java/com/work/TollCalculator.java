package com.work;

import com.work.exceptions.MissingHolidayDataException;
import com.work.model.TollFeeTime;
import com.work.model.vehicles.Vehicle;
import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.time.LocalTime;
import java.time.ZoneId;
import java.util.*;
import java.util.concurrent.TimeUnit;

public class TollCalculator {

  private static final int MAX_FEE_PER_DAY = 60;
  private List<TollFeeTime> tollFeeTimes = null;

  /**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
  public int getTollFee(Vehicle vehicle, Date... dates) throws MissingHolidayDataException, IOException {

    if(dates == null || dates.length == 0) return 0;
    if(vehicle != null && vehicle.isTollFree()) return 0;
    if(isTollFreeDate(dates[0]))  return 0;
    if(dates.length == 1){
       return getTollFee(dates[0]);
    }

    Date intervalStart = dates[0];
    int totalFee = 0;
    int tempFee = 0;

    Arrays.sort(dates);


    for (Date date : dates) {

      int nextFee = getTollFee(date);

      TimeUnit timeUnit = TimeUnit.MINUTES;
      long diffInMillies = date.getTime() - intervalStart.getTime();
      long minutes = timeUnit.convert(diffInMillies, TimeUnit.MILLISECONDS);

      if (minutes <= 60) {
        if(nextFee >= tempFee) {
          totalFee -= tempFee;
          totalFee += nextFee;
          tempFee = nextFee;
        }

      } else {
        intervalStart = date;
        tempFee = nextFee;
        totalFee += nextFee;
      }
      if (totalFee >= MAX_FEE_PER_DAY){
        return MAX_FEE_PER_DAY;
      }

    }
    return totalFee;
  }

  private List<TollFeeTime> getFeeAndTimes() throws IOException {
    List<TollFeeTime> tollFeeTimesList = new ArrayList<>();
    try{
      JSONObject jsonObject = new JSONObject(Files.readString(Paths.get("src/main/resources/TollFees.json")));
      JSONArray feeTimes =  jsonObject.getJSONArray("feeTimes");
      feeTimes.forEach(feeTime -> {
        JSONObject jsonFeeTime = (JSONObject) feeTime;
        JSONArray timeArray = jsonFeeTime.getJSONArray("time");
        timeArray.forEach(time -> {
          TollFeeTime tollFeeTime = new TollFeeTime();
          JSONObject jsonTime = (JSONObject) time;
          tollFeeTime.setStartTime(LocalTime.parse(jsonTime.getString("startTime")));
          tollFeeTime.setEndTime(LocalTime.parse(jsonTime.getString("endTime")));
          tollFeeTime.setFee(jsonFeeTime.getInt("fee"));
          tollFeeTimesList.add(tollFeeTime);
        });
      });
    } catch (Exception e){
      throw new RuntimeException("Exception occurred in getting fee from json", e);
    }

    return tollFeeTimesList;
  }

  private int getTollFee(final Date date) throws IOException {

    if(tollFeeTimes == null){
      tollFeeTimes = getFeeAndTimes();
    }

    LocalTime localTime = LocalTime.ofInstant(date.toInstant(), ZoneId.systemDefault());
    Optional<TollFeeTime> optionalTollFeeTime=  tollFeeTimes.stream().filter(tollFeeTime ->
     tollFeeTime.getStartTime().equals(localTime) || tollFeeTime.getEndTime().equals(localTime) ||
             (localTime.isAfter(tollFeeTime.getStartTime()) && localTime.isBefore(tollFeeTime.getEndTime()))).findFirst();
    return optionalTollFeeTime.map(TollFeeTime::getFee).orElse(0);
  }

  private boolean isTollFreeDate(Date date) throws MissingHolidayDataException {
    Calendar calendar = GregorianCalendar.getInstance();
    calendar.setTime(date);

    int dayOfWeek = calendar.get(Calendar.DAY_OF_WEEK);
    if (dayOfWeek == Calendar.SATURDAY || dayOfWeek == Calendar.SUNDAY) return true;

    int year = calendar.get(Calendar.YEAR);
    int month = calendar.get(Calendar.MONTH);
    int day = calendar.get(Calendar.DAY_OF_MONTH);
    return isHoliday(year, month, day);
  }

  private boolean isHoliday(int year, int month, int day) throws MissingHolidayDataException {

    try {
      JSONObject jsonObject = new JSONObject(Files.readString(Paths.get("src/main/resources/Holidays.json")));
      JSONObject holidays = jsonObject.getJSONObject(String.valueOf(year));
      if(holidays != null && !holidays.isEmpty()){
        String monthlyHolidays = holidays.getString(getMonth(month));
        String[] dateArray = monthlyHolidays.split(",");
        if(Arrays.asList(dateArray).contains(String.valueOf(day)))
          return true;
      }

    }catch (Exception e){
      throw new MissingHolidayDataException(e.getCause());
    }
    return false;
  }

  private String getMonth (int monthNum){
    switch (monthNum){
      case 0: return "January";
      case 1: return "February";
      case 2: return "March";
      case 3: return "April";
      case 4: return "May";
      case 5: return "June";
      case 6: return "July";
      case 7: return "August";
      case 8: return "September";
      case 9: return "October";
      case 10: return "November";
      case 11: return "December";
      default : return "Error";
    }
  }

}

