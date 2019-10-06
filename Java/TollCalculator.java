import static java.time.temporal.ChronoUnit.HOURS;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.LocalTime;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Set;
import java.util.SortedSet;
import java.util.TreeSet;
import java.util.stream.Collectors;

public class TollCalculator {
	
	private static final int maxFeePerDay = 60;

/**
   * Calculate the total toll fee for one day
   *
   * @param vehicle - the vehicle
   * @param dates   - date and time of all passes on one day
   * @return - the total toll fee for that day
   */
	public int getTollFee(Vehicle vehicle, Date... dates) {
		if(dates == null || dates.length == 0)
			return 0;
		
		List<List<Date>> datesGroupedByDay = groupDatesByDay(dates);
		
		int totalFee = 0;
		for(List<Date> datesByDay : datesGroupedByDay) {
			SortedSet<Date> datesByDaySet = new TreeSet<>(datesByDay);
			int totalDayFee = 0;
			LocalTime intervalStartTime = null;
			int highestIntervalFee = 0;
			
			for (Date date : datesByDaySet) {				
				LocalTime currentTime = toLocalTime(date);
				int currentFee = getTollFee(date, vehicle);

				if(newInterval(intervalStartTime, currentTime) || highestIntervalFee < currentFee) {
					if(newInterval(intervalStartTime, currentTime))
						intervalStartTime = currentTime;
					else if(highestIntervalFee < currentFee)
						totalDayFee -= highestIntervalFee;
					highestIntervalFee = currentFee;
					totalDayFee += highestIntervalFee;
				}
				
				if(totalDayFee >= maxFeePerDay) {
					totalDayFee = maxFeePerDay;
					break;
				}
			}
			totalFee += totalDayFee;
		}
		
		return totalFee;
	}

	public boolean newInterval(LocalTime intervalStartTime, LocalTime currentTime) {
		return intervalStartTime == null || HOURS.between(intervalStartTime, currentTime) >= 1;
	}

	public boolean isTollFreeVehicle(Vehicle vehicle) {
		if(vehicle == null) return false;
		return vehicle.isTollFreeVehicle();
	}

	public int getTollFee(final Date date, Vehicle vehicle) {
		LocalDate localDate = toLocalDate(date);
		LocalTime localTime = toLocalTime(date);
		
		if(isFeeFreeDayOrVehicle(localDate, vehicle))
			return 0;
		
		if (localTime.isAfter(LocalTime.of(5, 59)) && localTime.isBefore(LocalTime.of(6, 30))) return 8;
		else if (localTime.isAfter(LocalTime.of(6, 29)) && localTime.isBefore(LocalTime.of(7, 0))) return 13;
		else if (localTime.isAfter(LocalTime.of(6, 59)) && localTime.isBefore(LocalTime.of(8, 0))) return 18;
		else if (localTime.isAfter(LocalTime.of(7, 59)) && localTime.isBefore(LocalTime.of(8, 30))) return 13;
		else if (localTime.isAfter(LocalTime.of(8, 29)) && localTime.isBefore(LocalTime.of(15, 0))) return 8;
		else if (localTime.isAfter(LocalTime.of(14, 59)) && localTime.isBefore(LocalTime.of(15, 30))) return 13;
		else if (localTime.isAfter(LocalTime.of(15, 29)) && localTime.isBefore(LocalTime.of(17, 0))) return 18;
		else if (localTime.isAfter(LocalTime.of(16, 59)) && localTime.isBefore(LocalTime.of(18, 0))) return 13;
		else if (localTime.isAfter(LocalTime.of(17, 59)) && localTime.isBefore(LocalTime.of(18, 30))) return 8;
		else return 0;
	}
	
	public boolean isFeeFreeDayOrVehicle(LocalDate localDate, Vehicle vehicle) {
		Set<DayOfWeek> tollFreeDays = new HashSet<>(Arrays.asList(DayOfWeek.SATURDAY, DayOfWeek.SUNDAY));
		SwedishHolidays holidays = new SwedishHolidays(localDate.getYear());
		return tollFreeDays.contains(localDate.getDayOfWeek()) || isTollFreeVehicle(vehicle) || holidays.isHoliday(localDate);
	}
  
  	public LocalDate toLocalDate(Date date) {
	    return date.toInstant().atZone(ZoneId.systemDefault()).toLocalDate();
	}
  	
  	public LocalTime toLocalTime(Date date) {
	    return date.toInstant().atZone(ZoneId.systemDefault()).toLocalTime();
	}

	public List<List<Date>> groupDatesByDay(Date[] dates) {
		List<Date> datesList = new ArrayList<>(Arrays.asList(dates));

		return new ArrayList<>(datesList.stream().filter(v -> v != null).sorted().collect(
				Collectors.groupingBy(d -> d.toInstant().atZone(ZoneId.systemDefault()).toLocalDate(),
				LinkedHashMap::new, Collectors.toList())).values());
	}
}

